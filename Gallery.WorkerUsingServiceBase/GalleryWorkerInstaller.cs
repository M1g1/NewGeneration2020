using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace Gallery.Worker
{
    [RunInstaller(true)]
    public partial class GalleryWorkerInstaller : System.Configuration.Install.Installer
    {
        private readonly ServiceInstaller serviceInstaller = new ServiceInstaller();
        private readonly ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
        public GalleryWorkerInstaller()
        {
            InitializeComponent();
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "GalleryWorkerService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }

    }
}
