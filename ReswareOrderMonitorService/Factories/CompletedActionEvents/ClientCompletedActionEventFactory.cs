using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Factories.StatusSendersOrders;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal abstract class ClientCompletedActionEventFactory : IClientCompletedActionEventFactory
    {
        protected internal IStatusSenderOrderFactory StatusSenderOrderFactory;

        internal ClientCompletedActionEventFactory() : this(ReswareOrderDependencyFactory.Resolve<IStatusSenderOrderFactory>()) { }

        internal ClientCompletedActionEventFactory(IStatusSenderOrderFactory statusSenderOrderFactory)
        {
            StatusSenderOrderFactory = statusSenderOrderFactory;
        }

        public abstract IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
