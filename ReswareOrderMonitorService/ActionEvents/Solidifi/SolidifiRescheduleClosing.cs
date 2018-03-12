using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Mirth;
using ReswareOrderMonitorService.Repositories;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Solidifi
{
    internal class SolidifiRescheduleClosing : SchedulingReschedule
    {
        internal SolidifiRescheduleClosing(IServiceUtility orderServiceUtility, IIntegrationServiceRepository integrationServiceRepository, IReceiveSigningServiceRepository receiveSigningServiceRepository, IMirthServiceClient mirthServiceClient) : base(orderServiceUtility, integrationServiceRepository, receiveSigningServiceRepository, mirthServiceClient) { }

        internal override bool PerformAction(OrderResult order)
        {
            var existingOrder = IntegrationServiceRepository.GetOrder(order.CustomerId, order.FileNumber);
            if (existingOrder.Outcome == OutcomeEnum.Fail || existingOrder.Order == null)
            {
                order.Notes += $"Received Reschedule Action Event from Resware for file number {order.FileNumber}.";
            }

            return new SolidifiRequestClosing(ReceiveSigningServiceRepository, MirthServiceClient, OrderServiceUtility).PerformAction(order);
        }
    }
}
