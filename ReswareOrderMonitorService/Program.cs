using System;
using System.Diagnostics;
using System.ServiceProcess;
using ReswareOrderMonitorService.Aspose;
using ReswareOrderMonitorService.Factories.DependencyFactory;
using ReswareOrderMonitorService.Monitors.Documents;
using ReswareOrderMonitorService.Monitors.OrderActionEvents;
using ReswareOrderMonitorService.Monitors.Outgoing;

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
                    DependencyFactory.Resolve<OrderActionEventMonitor>(), 
                    DependencyFactory.Resolve<DocumentMonitor>(), 
                    DependencyFactory.Resolve<OutgoingMonitor>())
                };

                ServiceBase.Run(servicesToRun);
#endif

                DependencyFactory.Resolve<OrderActionEventMonitor>().MonitorOrderActionEvents();
                DependencyFactory.Resolve<DocumentMonitor>().MonitorDocuments();
                DependencyFactory.Resolve<OutgoingMonitor>().MonitorOrders();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
