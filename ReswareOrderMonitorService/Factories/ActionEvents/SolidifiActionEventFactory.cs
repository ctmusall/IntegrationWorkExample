using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Common.Solidifi;
using ReswareOrderMonitorService.Factories.Services;

namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal class SolidifiActionEventFactory : ActionEventFactory
    {
        internal SolidifiActionEventFactory(IServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RescheduleClosing:
                    return new SolidifiRescheduleClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiRequestClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiRequestTitleOpinion(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.TitleOpinion));
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiRequestDocPrep(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.DocPrep));
                case SolidifiActionEventConstants.FundingAuth:
                    return new SolidifiFundingAuth();
                 default:
                    return null;   
            }
        }
    }
}
