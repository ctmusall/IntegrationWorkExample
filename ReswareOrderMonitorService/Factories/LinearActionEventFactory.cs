using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;
using ReswareOrderMonitorService.Common;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearActionEventFactory : ActionEventFactory
    {
        internal LinearActionEventFactory(IServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        private const string RequestClosing = "234";
        private const string RescheduleClosing = "240";
        private const string RequestTitleOpinion = "235"; // TODO - Change to linear request title opinion action event
        private const string RequestDocPrep = "236"; // TODO - Change to linear request deed action event
        private const string FundingAuth = "237"; // TODO - Change to linear receive funding auth action event

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case RescheduleClosing:
                    return new LinearRescheduleClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestClosing:
                    return new LinearRequestClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestTitleOpinion:
                    return new LinearRequestTitleOpinion(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.TitleOpinion));
                case RequestDocPrep:
                    return new LinearRequestDocPrep(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.DocPrep));
                case FundingAuth:
                    return new LinearFundingAuth();
                 default:
                    return null;   
            }
        }
    }
}
