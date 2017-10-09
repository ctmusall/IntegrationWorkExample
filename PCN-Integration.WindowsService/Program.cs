using System.ServiceProcess;

namespace PCN_Integration.WindowsService
{
    static class Program
    {
        /// <summary>
        /// Creates array containing integration service and calls the static ServiceBase.Run() method to begin processing.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new IntegrationService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
