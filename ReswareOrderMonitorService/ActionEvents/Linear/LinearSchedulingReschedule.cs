using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.ActionEvents.Linear
{
    internal class LinearSchedulingReschedule : SchedulingReschedule
    {
        internal LinearSchedulingReschedule(IOrderServiceUtility orderServiceUtility) : base(orderServiceUtility)
        {
        }

        internal override bool PerformAction(OrderResult order)
        {
            var existingOrder = IntegrationServiceClient.GetOrder(order.CustomerId, order.FileNumber);
            if (existingOrder.Outcome == OutcomeEnum.Fail || existingOrder.Order == null)
            {
                order.Notes += $"Received Reschedule Action Event from Resware for file number {order.FileNumber}.";
            }

            return new LinearRequestClosing(OrderServiceUtility).PerformAction(order);
        }
    }
}
