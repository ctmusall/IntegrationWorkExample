using Resware.Entities.Orders;
using ReswareOrderMonitorService.StatusSenders;

namespace ReswareOrderMonitorService.Factories.StatusSenders
{
    internal interface IStatusSenderFactory
    {
        IStatusSender ResolveStatusSender(Order order);
    }
}
