using ReswareOrderMonitorService.ReswareOrders;
using ReswareOrderMonitorService.StatusSenders;

namespace ReswareOrderMonitorService.Factories.StatusSenders
{
    internal interface IStatusSenderFactory
    {
        IStatusSender ResolveStatusSender(OrderResult order);
    }
}
