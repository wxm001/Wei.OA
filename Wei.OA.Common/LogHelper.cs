using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Common
{
    using System.Threading;

    //public delegate void WriteLogDel(string str);
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue=new Queue<string>();
        public static List<ILogWriter> LogWriterList=new List<ILogWriter>();

        //public static WriteLogDel WriteLogDelFunc;

        static LogHelper()
        {
            //WriteLogDelFunc=new WriteLogDel(WriteLogToFile);
            //WriteLogDelFunc += WriteLogToDb;

            LogWriterList.Add(new TextFileWriter());
            LogWriterList.Add(new SqlServerWriter());
            LogWriterList.Add(new Log4NetWriter());

            //从队列中获取错误消息写到日志文件中
            ThreadPool.QueueUserWorkItem(
                o =>
                    {
                        while (true)
                        {
                            lock (ExceptionStringQueue)
                            {
                                if (ExceptionStringQueue.Count > 0)
                                {
                                    string str = ExceptionStringQueue.Dequeue();

                                    //把异常信息写到日志文件；变化点：可能写到日志文件或者数据库或者都写。
                                    //观察者模式
                                    foreach (var logWriter in LogWriterList)
                                    {
                                        logWriter.WriteLogInfo(str);
                                    }
                                }
                                else
                                {
                                    Thread.Sleep(30);
                                }
                            } 
                        }
                    });
        }

        //public static void WriteLogToFile(string txt)
        //{

        //}

        //public static void WriteLogToDb(string txt)
        //{

        //}
        /// <summary>
        /// 错误消息写入队列
        /// </summary>
        /// <param name="exceptionText"></param>
        public static void WriteLog(string exceptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        }
    }
}
