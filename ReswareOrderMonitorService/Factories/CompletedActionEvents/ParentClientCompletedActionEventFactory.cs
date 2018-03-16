using ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi;
using ReswareOrderMonitorService.Repositories;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents
{
    internal class ParentClientCompletedActionEventFactory : IParentClientCompletedActionEventFactory
    {
        public IClientCompletedActionEventFactory ResolveClientCompletedActionEventFactory(int clientId)
        {
            switch (clientId)
            {
                // TODO - Switch on client ID
                default:
                    return new SolidifiCompletedActionEventFactory(new IntegrationServiceRepository());
            }
        }
    }
}
