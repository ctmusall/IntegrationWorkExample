using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;
using ReswareOrderMonitorService.Common;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearActionEventFactory : ActionEventFactory
    {
        internal LinearActionEventFactory(IServiceUtilityFactory serviceUtilityFactory) : base(serviceUtilityFactory) { }

        private const string RequestClosingActionEventCode = "234";
        private const string RescheduleActionEventCode = "240";
        private const string RequestTitleOpinion = "235"; // TODO - Change to linear request title opinion action event
        private const string RequestDocPrep = "236"; // TODO - Change to linear request deed action event

        internal override ActionEvent ResolveActionEvent(string actionEventCode)
        {
            switch (actionEventCode)
            {
                case RescheduleActionEventCode:
                    return new LinearSchedulingReschedule(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestClosingActionEventCode:
                    return new LinearRequestClosing(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.Closing));
                case RequestTitleOpinion:
                    return new LinearRequestTitleOpinion(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.TitleOpinion));
                case RequestDocPrep:
                    return new LinearRequestDocPrep(ServiceUtilityFactory.ResolveServiceUtility(ServiceUtilityTypeEnum.DocPrep));
                 default:
                    return null;   
            }
        }
    }
}
