using System;
using System.Diagnostics;
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
            try
            {
                var servicesToRun = new ServiceBase[]
                {
                    new IntegrationService()
                };
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }

        }
    }
}
