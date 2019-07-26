/*============================================
*类名称：Log4NetWriter
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Common
{
    using log4net;

    /// <summary>
    /// Log4NetWriter
    /// </summary>
    public class Log4NetWriter:ILogWriter
    {
        public void WriteLogInfo(string txt)
        {
            ILog logWrite = log4net.LogManager.GetLogger("Demo");
            logWrite.Error(txt);
        }
    }
}
