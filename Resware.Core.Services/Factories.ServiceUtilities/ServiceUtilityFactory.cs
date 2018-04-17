using Resware.Core.Services.Utilities.ServiceUtilities;
using ReswareCommon.Enums;

namespace Resware.Core.Services.Factories.ServiceUtilities
{
    public abstract class ServiceUtilityFactory
    {
        public abstract ServiceUtility ResolveServiceUtility(OrderTypeEnum orderType);
    }
}
