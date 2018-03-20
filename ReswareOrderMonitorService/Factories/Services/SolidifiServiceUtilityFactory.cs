using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Solidifi;

namespace ReswareOrderMonitorService.Factories
{
    internal class SolidifiServiceUtilityFactory : ServiceUtilityFactory
    {
        public override IServiceUtility ResolveServiceUtility(OrderTypeEnum serviceUtilityType)
        {
            switch (serviceUtilityType)
            {
                case OrderTypeEnum.Closing:
                    return new SolidifiClosingServiceUtility();
                case OrderTypeEnum.TitleOpinion:
                    return new SolidifiTitleOpinionServiceUtility();
                case OrderTypeEnum.DocPrep:
                    return new SolidifiDocPrepServiceUtility();
                default:
                    return null;
            }
        }
    }
}
