using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents
{
    internal abstract class SchedulingReschedule : ActionEvent
    {
        protected internal IIntegrationServiceRepository IntegrationServiceRepository;
        protected internal IServiceUtility OrderServiceUtility;
        protected internal IReceiveSigningServiceRepository ReceiveSigningServiceRepository;
        protected internal IMirthServiceClient MirthServiceClient;

        internal SchedulingReschedule(IServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient)
        {
            OrderServiceUtility = orderServiceUtility;
            IntegrationServiceRepository = integrationServiceRepository;
            ReceiveSigningServiceRepository = receiveSigningServiceRepository;
            MirthServiceClient = mirthServiceClient;
        }
    }
}
