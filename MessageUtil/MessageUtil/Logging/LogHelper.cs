using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtil.Logging
{
    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message, string timestamp)
        {
            switch (target)
            {
                case LogTarget.File:
                    FileLogger.Instance.Log(message, timestamp);
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

        //Method for logging Exceptions is purposely put separated here so it can be modified depending 
        //on the desired way for ExceptionLogging
        public static void LogException(LogTarget target, Exception ex, string timestamp)
        {
            switch (target)
            {
                case LogTarget.File:
                    {
                        //Only File Logger is working for now but this is a good base for extension
                        //So this is the only ExceptionLogger that will be implemented
                        string content = String.Format("!![Exception]: {0}", ex.StackTrace);
                        FileLogger.Instance.Log(content, timestamp);
                        break;
                    }
                case LogTarget.Database:
                    break;
                case LogTarget.EventLog:
                    break;
                default:
                    return;
            }
        }
    }
}
