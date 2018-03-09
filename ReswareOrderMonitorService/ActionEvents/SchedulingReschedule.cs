using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class SchedulingReschedule : ActionEvent
    {
        protected internal IntegrationServiceClient IntegrationServiceClient;
        protected internal IServiceUtility OrderServiceUtility;

        internal SchedulingReschedule(IServiceUtility orderServiceUtility)
        {
            OrderServiceUtility = orderServiceUtility;
            IntegrationServiceClient = new IntegrationServiceClient();
        }
    }
}
