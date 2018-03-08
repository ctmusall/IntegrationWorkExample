using ReswareOrderMonitorService.Common.Solidifi;
using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi
{
    internal class SolidifiCompletedActionEventFactory : ClientCompletedActionEventFactory
    {
        public override IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiClosingStatusSenderFactory(IntegrationServiceClient.GetOrder(customerId, fileNumber));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiTitleOpinionStatusSenderFactory(IntegrationServiceClient.GetOrder(customerId, $"{fileNumber}-T"));
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiDocPrepStatusSenderFactory(IntegrationServiceClient.GetOrder(customerId, $"{fileNumber}-D"));
                default:
                    return null;
            }
        }

    }
}
