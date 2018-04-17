using eClosings.Data.IntegrationService.Repository;
using Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents;

namespace Resware.Core.ActionEvent.Factories.ParentClientCompletedActionEvents
{
    public class ParentClientCompletedActionEventFactory : IParentClientCompletedActionEventFactory
    {
        private readonly IIntegrationServiceRepository _integrationServiceRepository;

        public ParentClientCompletedActionEventFactory() : this(DependencyFactory.Resolve<IIntegrationServiceRepository>()) { }

        internal ParentClientCompletedActionEventFactory(IIntegrationServiceRepository integrationServiceRepository)
        {
            _integrationServiceRepository = integrationServiceRepository;
        }

        public IClientCompletedActionEventFactory ResolveClientCompletedActionEventFactory(int clientId)
        {
            switch (clientId)
            {
                case 1:
                    return new SolidifiCompletedActionEventFactory(_integrationServiceRepository);
                default:
                    return null;
            }
        }
    }
}
