using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class ServiceUtilityFactory : IServiceUtilityFactory
    {
        public abstract IServiceUtility ResolveServiceUtility(OrderTypeEnum orderType);
    }
}
