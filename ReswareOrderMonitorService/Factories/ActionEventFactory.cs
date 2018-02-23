using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Factories
{
    internal abstract class ActionEventFactory
    {
        internal ActionEventFactory(string closingCompletedEventCode, string receiveFundingEventCode,string notaryDocumentsPickedUpEventCode, string requestClosingEventCode,string schedulingRescheduleEventCode)
        {
            ClosingCompletedEventCode = closingCompletedEventCode;
            ReceiveFundingAuthEventCode = receiveFundingEventCode;
            NotaryDocumentsPickedUpEventCode = notaryDocumentsPickedUpEventCode;
            RequestClosingEventCode = requestClosingEventCode;
            SchedulingRescheduleEventCode = schedulingRescheduleEventCode;
        }

        protected internal string ClosingCompletedEventCode { get; set; }
        protected internal string ReceiveFundingAuthEventCode { get; set; }
        protected internal string NotaryDocumentsPickedUpEventCode { get; set; }
        protected internal string RequestClosingEventCode { get; set; }
        protected internal string SchedulingRescheduleEventCode { get; set; }

        internal abstract ActionEvent ResolveActionEvent(ActionEventServiceResult actionEvent);
    }
}
