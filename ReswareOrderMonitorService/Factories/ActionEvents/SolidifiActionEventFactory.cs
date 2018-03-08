using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Solidifi;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Factories.Services;

namespace ReswareOrderMonitorService.Factories.ActionEvents
{
    internal class SolidifiActionEventFactory : ActionEventFactory
    {
        internal SolidifiActionEventFactory(IServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        private const string RequestClosing = "234";
        private const string RescheduleClosing = "240";
        private const string RequestTitleOpinion = "235"; // TODO - Change to Solidifi request title opinion action event
        private const string RequestDocPrep = "236"; // TODO - Change to Solidifi request deed action event
        private const string FundingAuth = "237"; // TODO - Change to Solidifi receive funding auth action event

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case RescheduleClosing:
                    return new SolidifiRescheduleClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestClosing:
                    return new SolidifiRequestClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestTitleOpinion:
                    return new SolidifiRequestTitleOpinion(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.TitleOpinion));
                case RequestDocPrep:
                    return new SolidifiRequestDocPrep(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.DocPrep));
                case FundingAuth:
                    return new SolidifiFundingAuth();
                 default:
                    return null;   
            }
        }
    }
}
