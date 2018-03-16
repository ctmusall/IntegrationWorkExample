using ReswareOrderMonitorService.Factories.Services;

namespace ReswareOrderMonitorService.Factories
{
    internal interface IParentServiceUtilityFactory
    {
        IServiceUtilityFactory ResolveServiceUtilityFactory(int clientId);
    }
}
