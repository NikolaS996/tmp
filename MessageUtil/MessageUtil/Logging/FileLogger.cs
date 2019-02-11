using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtil.Logging
{
    public class FileLogger : LogBase
    {
        public static string filePath = "D://Log.txt";


        //Singleton instance object
        private static FileLogger instance = null;

        //Making FileLogger Sigleton here
        private FileLogger()
        {

        }
        

        public override void Log(string message, string timestamp)
        {
            //GrantAccess(filePath);
            lock (lockObj)  
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, append: true))
                {
                    streamWriter.WriteLine("timestamp: { 0},\t{ 1}", timestamp, message);
                    streamWriter.Close();
                }
            }
        }

        private void GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        public static FileLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileLogger();
                }
                return instance;
            }
        }
    }
}
