using System.ServiceProcess;
using PCN_Integration.Services;
using PCN_Integration.Services.Services;
using PCN_Integration.Services.Services.IntegrationServiceFactory;
using PCN_Integration.WindowsService.Common;

namespace PCN_Integration.WindowsService
{
    internal partial class IntegrationService : ServiceBase
    {
        private readonly IServiceFactory _serviceFactory;

        internal IntegrationService(IServiceFactory serviceFactory = null)
        {
            InitializeComponent();
            _serviceFactory = serviceFactory ?? new ServiceFactory();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(PcnIntegrationServiceConstants.PcnIntegrationServiceStatusMessages.IntegrationStarted);
            _serviceFactory.ResolveIntegrationService(typeof(FassMonitor)).BeginIntegrationProcessing();
        }    

        protected override void OnStop()
        {
            EventLog.WriteEntry(PcnIntegrationServiceConstants.PcnIntegrationServiceStatusMessages.IntegrationStopped);
        }
    }
}
