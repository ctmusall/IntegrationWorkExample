using Resware.Core.Services.Utilities.ServiceUtilities;
using Resware.Core.Services.Utilities.ServiceUtilities.ClosingService;
using Resware.Core.Services.Utilities.ServiceUtilities.DocPrepService;
using Resware.Core.Services.Utilities.ServiceUtilities.TitleOpinionService;
using ReswareCommon.Enums;

namespace Resware.Core.Services.Factories.ServiceUtilities
{
    internal class SolidifiServiceUtilityFactory : ServiceUtilityFactory
    {
        public override ServiceUtility ResolveServiceUtility(OrderTypeEnum serviceUtilityType)
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
