using System;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.Models;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class ClosingServiceUtility : IOrderServiceUtility
    {
        public virtual void AssignServices(RequestClosingMessage requestClosingMessage)
        {
            if (!string.Equals(requestClosingMessage.ClosingState, requestClosingMessage.BorrowerState, StringComparison.CurrentCultureIgnoreCase))
            {
                requestClosingMessage.Notes += $"Did not apply any services because Closing state '{requestClosingMessage.ClosingState}' is not equal to borrower state '{requestClosingMessage.BorrowerState}'.";
                return;
            }

            switch (requestClosingMessage.ClosingState)
            {
                case StateConstants.Delaware:
                    DetermineDelawareServices(requestClosingMessage);
                    return;
                case StateConstants.Georgia:
                case StateConstants.Massachusetts:
                case StateConstants.NorthCarolina:
                case StateConstants.NewYork:
                case StateConstants.Vermont:
                case StateConstants.WestVirginia:
                default:
                    return;
            }
        }

        internal virtual void DetermineDelawareServices(RequestClosingMessage requestClosingMessage) { }


    }
}
