using System.ServiceProcess;
using System.Timers;
using ReswareOrderMonitorService.Monitors;

namespace ReswareOrderMonitorService
{
    internal partial class ReswareMonitor : ServiceBase
    {
        private readonly Timer _timer;
        private readonly IOrderActionEventMonitor _orderActionEventMonitor;
        private readonly IDocumentMonitor _documentMonitor;
        private readonly IOutgoingMonitor _outgoingMonitor;

        internal ReswareMonitor(IOrderActionEventMonitor orderActionEventMonitor, IDocumentMonitor documentMonitor, IOutgoingMonitor outgoingMonitor)
        {
            InitializeComponent();
            _orderActionEventMonitor = orderActionEventMonitor;
            _documentMonitor = documentMonitor;
            _outgoingMonitor = outgoingMonitor;
            _timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Resware monitor started");

            _timer.Enabled = true;
            _timer.Interval = 120000;
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _orderActionEventMonitor.MonitorOrderActionEvents();
            _documentMonitor.MonitorDocuments();
            _outgoingMonitor.MonitorOrders();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Resware monitor stopped");
        }
    }
}
