namespace ReswareOrderMonitorService.Factories.OrderStatusSenders
{
    internal class ClientOrderStatusSenderFactory : IClientOrderStatusSenderFactory
    {
        public IOrderStatusSenderFactory ResolveClientOrderStatusReaderFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - Switch on the client ID
                default:
                    return new SolidifiOrderStatusSenderFactory();
            }
        }
    }
}
