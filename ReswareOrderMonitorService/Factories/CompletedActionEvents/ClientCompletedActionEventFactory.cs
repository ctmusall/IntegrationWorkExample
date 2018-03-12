using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Repositories;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal abstract class ClientCompletedActionEventFactory : IClientCompletedActionEventFactory
    {
        protected internal IIntegrationServiceRepository IntegrationServiceRepository;

        internal ClientCompletedActionEventFactory() : this(new IntegrationServiceRepository()) { }

        internal ClientCompletedActionEventFactory(IIntegrationServiceRepository integrationServiceRepository)
        {
            IntegrationServiceRepository = integrationServiceRepository;
        }

        public abstract IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
