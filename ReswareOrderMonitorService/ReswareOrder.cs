using System.ServiceProcess;
using System.Timers;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Monitors;

namespace ReswareOrderMonitorService
{
    internal partial class ReswareOrder : ServiceBase
    {
        private readonly Timer _timer;

        internal ReswareOrder() : this(ReswareOrderDependencyFactory.Resolve<Timer>())
        {
            InitializeComponent();
        }

        internal ReswareOrder(Timer timer)
        {
            _timer = timer;
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Resware order monitor service started.");

            _timer.Enabled = true;
            _timer.Interval = 60000;
            _timer.Elapsed += TimerElapsed;
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ReswareOrderDependencyFactory.Resolve<IReswareOrderMonitor>().MonitorOrders();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Resware order monitor service stopped.");
        }
    }
}
