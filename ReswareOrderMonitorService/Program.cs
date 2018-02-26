using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

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
                    new ReswareOrder(new Timer()) 
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
