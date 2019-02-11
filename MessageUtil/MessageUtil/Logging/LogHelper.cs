using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Logger
{
    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message, string timestamp)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message, timestamp);
                    break;
                case LogTarget.Database:
                    logger = new DBLogger();
                    logger.Log(message, timestamp);
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(message, timestamp);
                    break;
                default:
                    return;
            }
        }
    }
}
