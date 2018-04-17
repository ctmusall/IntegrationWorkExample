using eClosings.Data.IntegrationService.Repository;
using Resware.Core.Status.Factories.StatusSender;
using Resware.Core.Status.Factories.StatusSender.ClosingStatus;
using Resware.Core.Status.Factories.StatusSender.DocPrepStatus;
using Resware.Core.Status.Factories.StatusSender.TitleOpinion;
using ReswareCommon.Constants.Solidifi;

namespace Resware.Core.ActionEvent.Factories.ClientCompletedActionEvents
{
    internal class SolidifiCompletedActionEventFactory : ClientCompletedActionEventFactory
    {
        internal SolidifiCompletedActionEventFactory(IIntegrationServiceRepository integrationServiceRepository) : base(integrationServiceRepository) { }

        public override StatusSenderFactory ResolveCompletedActionEventStatusSenderFactory(string actionEventCode, string customerId, string fileNumber)
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
