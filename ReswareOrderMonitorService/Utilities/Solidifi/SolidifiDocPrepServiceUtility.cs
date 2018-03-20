using ReswareCommon;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities.Solidifi
{
    internal class SolidifiDocPrepServiceUtility : ServiceUtility
    {
        public override void AssignServices(RequestMessage requestClosingMessage)
        {
            requestClosingMessage.Service1 = StateConstants.OneHourReviewStates.Contains(requestClosingMessage.ClosingState) 
                ? ServiceNameConstants.DeedPreparationAndReview 
                : ServiceNameConstants.DeedPreparation;
        }
    }
}
