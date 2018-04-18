using eClosings.Mirth.Messages;
using ReswareCommon.Constants;

namespace Resware.Core.Services.Utilities.ServiceUtilities.DocPrepService
{
    internal class SolidifiDocPrepServiceUtility : ServiceUtility
    {
        public override void AssignServices(ReswareRequestOrder requestClosingMessage)
        {
            requestClosingMessage.Service1 = StateConstants.OneHourReviewStates.Contains(requestClosingMessage.ClosingState) 
                ? ServiceNameConstants.DeedPreparationAndReview 
                : ServiceNameConstants.DeedPreparation;
        }
    }
}
