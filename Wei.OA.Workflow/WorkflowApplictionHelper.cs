namespace Wei.OA.Workflow
{
    using System;
    using System.Activities;
    using System.Activities.DurableInstancing;
    using System.Collections.Generic;
    using System.Configuration;
    using System.ServiceModel.Activities.Description;
    using System.Threading;

    using Wei.OA.Common;

    public class WorkflowApplictionHelper
    {
        private static readonly string Strsql;

        static WorkflowApplictionHelper()
        {
            Strsql = ConfigurationManager.ConnectionStrings["wfsql"].ConnectionString;
        }

        public static WorkflowApplication CreateWorkflowApp(Activity activity, Dictionary<string, object> dicParam)
        {
            AutoResetEvent autoResetEvent=new AutoResetEvent(false);
            WorkflowApplication wfApp;
            if (dicParam==null)
            {
                wfApp=new WorkflowApplication(activity);
            }
            else
            {
                wfApp=new WorkflowApplication(activity,dicParam);
            }

            wfApp.Idle += a => //当工作流停下来时执行此方法
                {
                    Console.WriteLine("工作流停下来了");
                    Common.LogHelper.WriteLog("工作流停下来了");
                };
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e2)
                {
                    Console.WriteLine("工作卸载进行持久化，书签被创建时就会执行序列化到数据库里面");
                    Common.LogHelper.WriteLog("工作卸载进行持久化");
                    return PersistableIdleAction.Unload;
                };
            wfApp.Unloaded += a =>
                {
                    autoResetEvent.Set();
                    Console.WriteLine("工作流被卸载");
                    Common.LogHelper.WriteLog("工作流被卸载");
                };
            wfApp.OnUnhandledException += a =>
                {
                    autoResetEvent.Set();
                    Console.WriteLine("出现了异常");
                    Common.LogHelper.WriteLog(a.UnhandledException.ToString());
                    return UnhandledExceptionAction.Terminate; //当出现未处理的异常时，直接结束工作流
                };
            wfApp.Aborted += a =>
                {
                    autoResetEvent.Set();
                    Console.WriteLine("Aborted");
                };
            //创建一个保存工作流实例的sqlstore对象
            SqlWorkflowInstanceStore store=new SqlWorkflowInstanceStore(Strsql);
            //在进行持久化时，保存到上面的对象配置的数据库中
            wfApp.InstanceStore = store;
            wfApp.Run();
            return wfApp;

        }

        public static WorkflowApplication ResumeBookMark(
            Activity activity,
            Guid instanceId,
            string bookMarkName,
            object value)
        {
            AutoResetEvent autoResetEvent=new AutoResetEvent(false);
            WorkflowApplication wfApp=new WorkflowApplication(activity);
            wfApp.Idle += a =>
                {
                    Console.WriteLine("工作流停下来了");
                };
            //工作流停下来时若返回的是Unload，那么就卸载当前工作流实例，持久化到数据库里
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e3)
                {
                    Console.WriteLine("工作流卸载进行持久化");
                    return PersistableIdleAction.Unload;
                };
            wfApp.Unloaded += a =>
                {
                    Common.LogHelper.WriteLog("工作流被卸载");
                };
            wfApp.OnUnhandledException += a =>
                {
                    autoResetEvent.Set();
                    LogHelper.WriteLog(a.ExceptionSource.ToString());
                    Console.WriteLine("出现了异常");
                    return UnhandledExceptionAction.Terminate;
                };
            wfApp.Aborted += a =>
                {
                    autoResetEvent.Set();
                    Console.WriteLine("Aborted");
                };
            //创建一个保存工作流实例的sqlstore对象
            SqlWorkflowInstanceStore store=new SqlWorkflowInstanceStore(Strsql);
            //在wfApp进行持久化时，保存到上面对象配置的数据库中
            wfApp.InstanceStore = store;
            //从数据库中加载当前工作流实例的状态
            wfApp.Load(instanceId);
            //让工作流沿着书签位置继续执行
            wfApp.ResumeBookmark(bookMarkName, value);
            return wfApp;
        }
    }
}