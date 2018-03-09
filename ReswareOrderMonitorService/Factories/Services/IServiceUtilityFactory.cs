using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories.Services
{
    internal interface IServiceUtilityFactory
    {
        IServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType);
    }
}
