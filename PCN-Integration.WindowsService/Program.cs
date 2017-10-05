using System.ServiceProcess;

namespace PCN_Integration.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
            new IntegrationService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
