using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Util.Logger
{
    public class FileLogger : LogBase
    {
        public static string filePath = "D://Log.txt";
        
        public override void Log(string message, string timestamp)
        {
            //GrantAccess(filePath);
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, append: true))
                {
                    streamWriter.WriteLine(message + "Timestamp: " + timestamp);
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
    }
}
