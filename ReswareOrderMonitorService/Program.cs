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
                    new ReswareOrderMonitor()
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
