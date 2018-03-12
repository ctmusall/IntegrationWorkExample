using System;
using System.Diagnostics;
using System.ServiceProcess;
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
                //var servicesToRun = new ServiceBase[]
                //{
                //    new ReswareMonitor() 
                //};

                //ServiceBase.Run(servicesToRun);

                ReswareOrderDependencyFactory.Resolve<IOrderActionEventMonitor>().MonitorOrderActionEvents();
                ReswareOrderDependencyFactory.Resolve<IDocumentMonitor>().MonitorDocuments();
                ReswareOrderDependencyFactory.Resolve<IOutgoingMonitor>().MonitorOrders();

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
