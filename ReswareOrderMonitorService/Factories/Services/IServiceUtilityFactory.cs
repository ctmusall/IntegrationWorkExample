using ReswareCommon.Enums;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories
{
    internal interface IServiceUtilityFactory
    {
        IServiceUtility ResolveServiceUtility(OrderTypeEnum orderType);
    }
}
