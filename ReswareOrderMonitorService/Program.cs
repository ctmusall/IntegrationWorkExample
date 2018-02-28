using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace ReswareOrderMonitorService
{
    internal static class Program
    {
        internal static void Main()
        {
            try
            {
                var servicesToRun = new ServiceBase[]
                {
                    new ReswareMonitor() 
                };

                ServiceBase.Run(servicesToRun);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }
    }
}
