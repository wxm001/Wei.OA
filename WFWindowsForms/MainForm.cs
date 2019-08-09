using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Activities.DurableInstancing;


namespace WFWindowsForms
{
    using System.Activities;

    public partial class MainForm : Form
    {
        private WorkflowApplication WFApp { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //SqlWorkflowInstanceStore store=new SqlWorkflowInstanceStore(@"Server=.\USER-20161011QY;database=WeiOA;uid=sa;pwd=951014");

            //WorkflowApplication wfApp=new WorkflowApplication(
            //   new StateActivity(),
            //   new Dictionary<string, object>() { { "MarkName", this.txtBookMarkName.Text } });

            WorkflowApplication wfApp = new WorkflowApplication(
                new StateMoneyActivity(),
                new Dictionary<string, object>() { { "InBookMarkName", this.txtBookMarkName.Text } });

            //wfApp.InstanceStore = store;
            wfApp.Idle += OnWfAppIdle;
            wfApp.OnUnhandledException += OnWfAppException;
            wfApp.Unloaded = a => { Console.WriteLine("Unloaded"); };
            wfApp.Aborted = a => { Console.WriteLine("Aborted"); };

            //wfApp.Persist();
            //wfApp.PersistableIdle = a => { return PersistableIdleAction.Unload; }; //用持久化必须有这个
            wfApp.Run();
            WFApp = wfApp;


        }

        private UnhandledExceptionAction OnWfAppException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("出现了未经处理的异常" + arg.UnhandledException.ToString());
            return UnhandledExceptionAction.Terminate;
        }

        private void OnWfAppIdle(WorkflowApplicationIdleEventArgs obj)
        {
            Console.WriteLine("工作流停下");
            
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //让工作流继续
            WFApp.ResumeBookmark(this.txtBookMarkName.Text, int.Parse(this.txtMoney.Text));
        }

        private void btnStateStart_Click(object sender, EventArgs e)
        {
            WorkflowApplication wfApp=new WorkflowApplication(new StateActivity());
            wfApp.Run();
        }
    }
}
