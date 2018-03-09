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
                AsposeLicense.SetPdfLicense();
                AsposeLicense.SetWordsLicense();
#if (!DEBUG)
                var servicesToRun = new ServiceBase[]
                {
                    new ReswareMonitor() 
                };

                ServiceBase.Run(servicesToRun);
#else
                ReswareOrderDependencyFactory.Resolve<IOrderActionEventMonitor>().MonitorOrderActionEvents();
                ReswareOrderDependencyFactory.Resolve<IDocumentMonitor>().MonitorDocuments();
                ReswareOrderDependencyFactory.Resolve<IOutgoingMonitor>().MonitorOrders();
#endif
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
