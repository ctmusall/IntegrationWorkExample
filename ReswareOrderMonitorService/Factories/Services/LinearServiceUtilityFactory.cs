using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Utilities;
using ReswareOrderMonitorService.Utilities.Linear;

namespace ReswareOrderMonitorService.Factories.Services
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
                    return new LinearDocPrepServiceUtility();
                default:
                    return null;
            }
        }
    }
}
