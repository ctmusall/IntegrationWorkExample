using ReswareOrderMonitorService.Common.Solidifi;
using ReswareOrderMonitorService.Factories.StatusSenders;
using ReswareOrderMonitorService.Factories.StatusSenders.Solidifi;
using ReswareOrderMonitorService.Repositories;

namespace ReswareOrderMonitorService.Factories.CompletedActionEvents.Solidifi
{
    internal class SolidifiCompletedActionEventFactory : ClientCompletedActionEventFactory
    {
        internal SolidifiCompletedActionEventFactory(IIntegrationServiceRepository integrationServiceRepository) : base(integrationServiceRepository) { }

        public override IStatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber)
        {
            switch (actionEventCode)
            {
                case SolidifiActionEventConstants.RequestClosing:
                    return new SolidifiClosingStatusSenderFactory(IntegrationServiceRepository.GetOrder(customerId, fileNumber));
                case SolidifiActionEventConstants.RequestTitleOpinion:
                    return new SolidifiTitleOpinionStatusSenderFactory(IntegrationServiceRepository.GetOrder(customerId, $"{fileNumber}-T"));
                case SolidifiActionEventConstants.RequestDocPrep:
                    return new SolidifiDocPrepStatusSenderFactory(IntegrationServiceRepository.GetOrder(customerId, $"{fileNumber}-D"));
                default:
                    return null;
            }
        }

    }
}
