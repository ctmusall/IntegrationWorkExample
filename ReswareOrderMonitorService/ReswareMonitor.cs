using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using ReswareOrderMonitorService.Monitors.Documents;
using ReswareOrderMonitorService.Monitors.OrderActionEvents;
using ReswareOrderMonitorService.Monitors.Outgoing;

namespace ReswareOrderMonitorService
{
    internal partial class ReswareMonitor : ServiceBase
    {
        private readonly Timer _timer;
        private readonly OrderActionEventMonitor _orderActionEventMonitor;
        private readonly DocumentMonitor _documentMonitor;
        private readonly OutgoingMonitor _outgoingMonitor;

        internal ReswareMonitor(OrderActionEventMonitor orderActionEventMonitor, DocumentMonitor documentMonitor, OutgoingMonitor outgoingMonitor)
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

        private async void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            await Task.Run(() => _orderActionEventMonitor.MonitorOrderActionEvents());
            await Task.Run(() => _documentMonitor.MonitorDocuments());
            await Task.Run(() => _outgoingMonitor.MonitorOrders());
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Resware monitor stopped");
        }
    }
}
