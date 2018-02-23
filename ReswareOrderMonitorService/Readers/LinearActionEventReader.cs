using System;
using System.Linq;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Readers
{
    internal class LinearActionEventReader : ActionEventReader
    {
        private const string _closingCompletedActionEventCode = "19";
        private const string _receiveFundingAuthActionEventCode = "22";
        private const string _notaryDocumentsPickedUpActionEventCode = "214";
        private const string _requestClosingActionEventCode = "234";
        private const string _schedulingReschedule = "240";


        public override bool CompleteAction(OrderResult order)
        {
            var actionEvents = ActionEvents.Where(actionEvent => string.Equals(actionEvent.FileNumber, order.FileNumber,StringComparison.CurrentCultureIgnoreCase));

            actionEvents.ForEach(actionEvent =>
            {

            });

            return true;
        }
    }
}
