namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal interface IClientOrderStatusSenderFactory
    {
        IOrderStatusSenderFactory ResolveClientOrderStatusReaderFactory(int clientId);
    }
}
