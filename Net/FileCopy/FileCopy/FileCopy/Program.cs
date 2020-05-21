using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace FileCopy
{
    public class Utilities
    {
        [System.Runtime.InteropServices.DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        public static void CopyFile2RemoteFolder(string from, string to, string username, string domain, string password)
        {
            IntPtr tokenHandle = new IntPtr(0);
            int returnValue = LogonUser(username, domain, password, 2, 0, ref tokenHandle);
            if (returnValue == -1) throw new Exception("Logon failed.");

            System.Security.Principal.WindowsImpersonationContext impersonatedUser = null;
            System.Security.Principal.WindowsIdentity wid = new System.Security.Principal.WindowsIdentity(tokenHandle);
            impersonatedUser = wid.Impersonate();

            System.IO.File.Copy(from, to, true);
            impersonatedUser.Undo();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string fromPath = ConfigurationManager.AppSettings.Get("from");
            string toPath = ConfigurationManager.AppSettings.Get("to");
            string userName = ConfigurationManager.AppSettings.Get("username");
            string password = ConfigurationManager.AppSettings.Get("password");
            string domain = ConfigurationManager.AppSettings.Get("domain");
            Utilities.CopyFile2RemoteFolder(fromPath, toPath, userName, domain, password);
        }
    }
}
