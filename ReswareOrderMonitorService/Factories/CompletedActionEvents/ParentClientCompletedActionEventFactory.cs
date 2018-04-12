using ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi;
using ReswareOrderMonitorService.Repositories;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal class ParentClientCompletedActionEventFactory : IParentClientCompletedActionEventFactory
    {
        private readonly IIntegrationServiceRepository _integrationServiceRepository;

        internal ParentClientCompletedActionEventFactory() : this(DependencyFactory.Resolve<IIntegrationServiceRepository>()) { }

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
