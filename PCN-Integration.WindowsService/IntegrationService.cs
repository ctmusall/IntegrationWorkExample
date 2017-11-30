using System.ServiceProcess;
using System.Timers;
using PCN_Integration.Services.Services;
using PCN_Integration.Services.Services.IntegrationServiceFactory;
using PCN_Integration.WindowsService.Common;

namespace PCN_Integration.WindowsService
{
    internal partial class IntegrationService : ServiceBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly Timer _timer;

        internal IntegrationService(IServiceFactory serviceFactory = null)
        {
            InitializeComponent();
            _serviceFactory = serviceFactory ?? new ServiceFactory();
            _timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(PcnIntegrationWindowsServiceConstants.PcnIntegrationServiceStatusMessages.IntegrationStarted);

            _timer.Enabled = true;
            _timer.Interval = 60000;
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _serviceFactory.ResolveIntegrationService(typeof(FassMonitor)).BeginIntegrationProcessing();
        }    

        protected override void OnStop()
        {
            EventLog.WriteEntry(PcnIntegrationWindowsServiceConstants.PcnIntegrationServiceStatusMessages.IntegrationStopped);
        }
    }
}