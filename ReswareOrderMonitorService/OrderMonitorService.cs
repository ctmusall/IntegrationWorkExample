using System.ServiceProcess;

namespace ReswareOrderMonitorService
{
    internal partial class OrderMonitorService : ServiceBase
    {
        internal OrderMonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Resware order monitor service started.");
        }

        protected override void OnStop()
        {
        }
    }
}
