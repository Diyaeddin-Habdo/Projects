using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // Running in console mode
                Console.WriteLine("Running in console mode...");
                DbBackupService service = new DbBackupService();
                service.StartInConsole();
            }
            else
            {
                // Running as a Windows Service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new DbBackupService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe DatabaseBackupService.exe
// C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe /u DatabaseBackupService.exe