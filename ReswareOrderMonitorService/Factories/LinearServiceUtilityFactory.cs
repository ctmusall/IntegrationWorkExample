using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Linear;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearServiceUtilityFactory : ServiceUtilityFactory
    {
        public override IOrderServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType)
        {
            switch (serviceUtilityType)
            {
                case ServiceUtilityTypeEnum.Closing:
                    return new LinearClosingServiceUtility();
                case ServiceUtilityTypeEnum.TitleOpinion:
                    return new LinearTitleOpinionServiceUtility();
                case ServiceUtilityTypeEnum.DocPrep:
                    return null;
                default:
                    return null;
            }
        }
    }
}
