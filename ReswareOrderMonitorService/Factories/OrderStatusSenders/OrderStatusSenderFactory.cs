using ReswareOrderMonitorService.OrderStatusSenders;

namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal abstract class OrderStatusSenderFactory : IOrderStatusSenderFactory
    {
        public abstract IStatusSender ResolveOrderStatusSender(string previousOrderStatus, string currentOrderStatus);
    }
}
