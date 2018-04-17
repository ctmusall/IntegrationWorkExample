using eClosings.Data.IntegrationService.Repository;
using Resware.Core.Status.Factories.StatusSender;

namespace Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents
{
    public abstract class ClientCompletedActionEventFactory : IClientCompletedActionEventFactory
    {
        protected internal IIntegrationServiceRepository IntegrationServiceRepository;

        internal ClientCompletedActionEventFactory() : this(new IntegrationServiceRepository()) { }

        internal ClientCompletedActionEventFactory(IIntegrationServiceRepository integrationServiceRepository)
        {
            IntegrationServiceRepository = integrationServiceRepository;
        }

        public abstract StatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber);
    }
}
