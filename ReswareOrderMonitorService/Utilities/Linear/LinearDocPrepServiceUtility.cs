using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities.Linear
{
    internal class LinearDocPrepServiceUtility : ServiceUtility
    {
        public override void AssignServices(RequestMessage requestClosingMessage)
        {
            requestClosingMessage.Service1 = StateConstants.OneHourReviewStates.Contains(requestClosingMessage.ClosingState) 
                ? ServiceNameConstants.DeedPreparationAndReview 
                : ServiceNameConstants.DeedPreparation;
        }
    }
}
