using System.ServiceProcess;
using System.Timers;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Monitors;

namespace ReswareOrderMonitorService
{
    internal partial class ReswareMonitor : ServiceBase
    {
        private readonly Timer _timer;

        public ReswareMonitor()
        {
            InitializeComponent();
            _timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Resware monitor started");

            _timer.Enabled = true;
            _timer.Interval = 60000;
            _timer.Elapsed += TimerElapsed;
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ReswareOrderDependencyFactory.Resolve<IOrderActionEventMonitor>().MonitorOrderActionEvents();
            ReswareOrderDependencyFactory.Resolve<IDocumentMonitor>().MonitorDocuments();
            ReswareOrderDependencyFactory.Resolve<IOutgoingMonitor>().MonitorOrders();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Resware monitor stopped");
        }
    }
}
