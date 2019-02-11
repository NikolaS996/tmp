using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Logger
{
    public class EventLogger : LogBase
    {
        public override void Log(string message, string timestamp)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog("");
                m_EventLog.Source = "MasazeriEventLog";
                m_EventLog.WriteEntry(message + "Timestamp: " + timestamp);
            }
        }
    }
}
