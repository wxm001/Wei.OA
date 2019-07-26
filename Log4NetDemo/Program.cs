using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    using log4net;

    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件中读取log4net配置，然后初始化
            log4net.Config.XmlConfigurator.Configure();

            //写日志
            ILog logWriter = log4net.LogManager.GetLogger("DemoWriter");
            logWriter.Debug("ssss调试级别的消息");
            logWriter.Error("sss错误级别的消息");
        }
    }
}
