using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories
{
    internal interface IServiceUtilityFactory
    {
        IServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType);
    }
}
