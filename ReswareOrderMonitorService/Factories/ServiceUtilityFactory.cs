using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;

namespace ReswareOrderMonitorService.Factories
{
    internal class ServiceUtilityFactory : IServiceUtilityFactory
    {
        public IOrderServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType)
        {
            switch (serviceUtilityType)
            {
                case ServiceUtilityTypeEnum.Closing:
                    return new LinearClosingServiceUtility();
                case ServiceUtilityTypeEnum.TitleOpinion:
                    return null;
                case ServiceUtilityTypeEnum.DocPrep:
                    return null;
                default:
                    return null;
            }
        }
    }
}
