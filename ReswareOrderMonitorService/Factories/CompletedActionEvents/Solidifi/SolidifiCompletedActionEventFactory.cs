using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Common.Solidifi;
using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Factories.StatusSendersOrders;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi
{
    internal class SolidifiCompletedActionEventFactory : ClientCompletedActionEventFactory
    {
        internal SolidifiCompletedActionEventFactory() { }
        internal SolidifiCompletedActionEventFactory(IStatusSenderOrderFactory statusSenderOrderFactory) : base(statusSenderOrderFactory) { }

        public override IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiClosingStatusSenderFactory(StatusSenderOrderFactory.ResolveOrderResult(ServiceUtilityTypeEnum.Closing, customerId, fileNumber));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiTitleOpinionStatusSenderFactory(StatusSenderOrderFactory.ResolveOrderResult(ServiceUtilityTypeEnum.TitleOpinion, customerId, fileNumber));
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiDocPrepStatusSenderFactory(StatusSenderOrderFactory.ResolveOrderResult(ServiceUtilityTypeEnum.DocPrep, customerId, fileNumber));
                default:
                    return null;
            }
        }


    }
}
