using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRescheduleClosing : SchedulingReschedule
    {
        internal SolidifiRescheduleClosing(IServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient) : base(orderServiceUtility, integrationServiceRepository, receiveSigningServiceRepository, mirthServiceClient) { }

        protected internal override RequestOrder ReturnNewClosing(IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient, IServiceUtility serviceUtility)
        {
            return new SolidifiRequestClosing(receiveSigningServiceRepository, mirthServiceClient, serviceUtility);
        }
    }
}
