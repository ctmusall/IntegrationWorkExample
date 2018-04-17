using System;
using System.Diagnostics;
using ReswareOrderMonitorService.Aspose;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Monitors;

namespace ReswareOrderMonitorService
{
    internal static class Program
    {
        internal static void Main()
        {
            try
            {
                AsposeLicense.SetLicenses();

#if (!DEBUG)
                var servicesToRun = new ServiceBase[]
                {
                    new ReswareMonitor(
                    DependencyFactory.Resolve<IOrderActionEventMonitor>(), 
                    DependencyFactory.Resolve<IDocumentMonitor>(), 
                    DependencyFactory.Resolve<IOutgoingMonitor>())
                };

                ServiceBase.Run(servicesToRun);
#endif

                DependencyFactory.Resolve<IOrderActionEventMonitor>().MonitorOrderActionEvents();
                DependencyFactory.Resolve<IDocumentMonitor>().MonitorDocuments();
                DependencyFactory.Resolve<IOutgoingMonitor>().MonitorOrders();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
