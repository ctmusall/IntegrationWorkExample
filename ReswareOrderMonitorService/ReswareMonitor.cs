using System.ServiceProcess;
using System.Timers;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Monitors;

namespace ReswareOrderMonitorService
{
    internal partial class ReswareMonitor : ServiceBase
    {
        private readonly Timer _timer;

        internal ReswareMonitor() : this(new Timer())
        {
            InitializeComponent();
        }

        internal ReswareMonitor(Timer timer)
        {
            _timer = timer;
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            EventLog.WriteEntry("Resware incoming service started");

            _timer.Enabled = true;
            _timer.Interval = 60000;
            _timer.Elapsed += TimerElapsed;
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            ReswareOrderDependencyFactory.Resolve<IOrderActionEventMonitor>().MonitorOrderActionEvents();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Resware incoming service stopped");
        }
    }
}
