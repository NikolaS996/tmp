using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageUtil.DataAccess;

namespace Util.Logger
{
    public class DBLogger : LogBase
    {
        string connectionString = DBFunctions.ConnectionString;
        public override void Log(string message, string timestamp)
        {
            lock (lockObj)
            {
                //Code to log data to the database
            }
        }
    }
}
