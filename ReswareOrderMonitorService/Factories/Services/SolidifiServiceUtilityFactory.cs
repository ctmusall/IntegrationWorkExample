using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories.Services
{
    internal class SolidifiServiceUtilityFactory : ServiceUtilityFactory
    {
        public override IServiceUtility ResolveServiceUtility(ServiceUtilityTypeEnum serviceUtilityType)
        {
            switch (serviceUtilityType)
            {
                case ServiceUtilityTypeEnum.Closing:
                    return new SolidifiClosingServiceUtility();
                case ServiceUtilityTypeEnum.TitleOpinion:
                    return new SolidifiTitleOpinionServiceUtility();
                case ServiceUtilityTypeEnum.DocPrep:
                    return new SolidifiDocPrepServiceUtility();
                default:
                    return null;
            }
        }
    }
}
