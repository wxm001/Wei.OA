using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Common
{
    public interface ILogWriter
    {
        void WriteLogInfo(string txt);
    }
}
