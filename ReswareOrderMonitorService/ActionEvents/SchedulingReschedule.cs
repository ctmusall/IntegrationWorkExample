using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class SchedulingReschedule : ActionEvent
    {
        protected internal IntegrationServiceClient IntegrationServiceClient;
        protected internal IOrderServiceUtility OrderServiceUtility;

        internal SchedulingReschedule(IOrderServiceUtility orderServiceUtility)
        {
            OrderServiceUtility = orderServiceUtility;
            IntegrationServiceClient = new IntegrationServiceClient();
        }
    }
}
