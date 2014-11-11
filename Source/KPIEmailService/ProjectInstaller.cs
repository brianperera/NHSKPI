
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace KPIEmailService
{
    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller _Process;
        private ServiceInstaller _Service;

        public ProjectInstaller()
        {
            _Process = new ServiceProcessInstaller();
            _Process.Account = ServiceAccount.LocalSystem;
            _Service = new ServiceInstaller();
            _Service.ServiceName = "KPIEmailWindowsService";
            Installers.Add(_Process);
            Installers.Add(_Service);
        }
    }
}
