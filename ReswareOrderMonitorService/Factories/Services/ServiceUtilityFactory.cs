using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Factories.Services;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class ServiceUtilityFactory : IServiceUtilityFactory
    {
        public abstract IServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType);
    }
}
