using ReswareOrderMonitorService.OrderStatusSenders;

namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal interface IOrderStatusSenderFactory
    {
        IStatusSender ResolveOrderStatusSender(string previousOrderStatus, string currentOrderStatus);
    }
}
