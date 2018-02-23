using System;
using System.Diagnostics;
using System.ServiceProcess;
using ReswareOrderMonitorService.Factories;

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
                    ReswareOrderDependencyFactory.Resolve<ReswareOrder>()
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
