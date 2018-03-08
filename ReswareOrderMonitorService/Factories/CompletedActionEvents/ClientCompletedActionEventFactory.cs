using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.Factories.StatusSenders;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal abstract class ClientCompletedActionEventFactory : IClientCompletedActionEventFactory
    {
        protected internal IntegrationServiceClient IntegrationServiceClient;

        internal ClientCompletedActionEventFactory() : this(new IntegrationServiceClient()) { }

        internal ClientCompletedActionEventFactory(IntegrationServiceClient integrationServiceClient)
        {
            IntegrationServiceClient = integrationServiceClient;
        }

        public abstract IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
