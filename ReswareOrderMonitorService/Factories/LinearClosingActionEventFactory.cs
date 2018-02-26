using ReswareOrderMonitorService.ActionEvents;
using ReswareOrderMonitorService.ActionEvents.Linear;
using ReswareOrderMonitorService.ReswareActionEvent;

namespace ReswareOrderMonitorService.Factories
{
    internal class LinearClosingActionEventFactory : ActionEventFactory
    {
        internal override ActionEvent ResolveActionEvent(ActionEventServiceResult actionEvent)
        {
            if (string.Equals(actionEvent.ActionEventCode, ClosingCompletedEventCode))
            {
                return new LinearClosingCompleted();
            }
            if (string.Equals(actionEvent.ActionEventCode, ReceiveFundingAuthEventCode))
            {
                return new LinearReceiveFundingAuth();
            }
            if (string.Equals(actionEvent.ActionEventCode, NotaryDocumentsPickedUpEventCode))
            {
                return new LinearNotaryDocumentsPickedUp();
            }
            if (string.Equals(actionEvent.ActionEventCode, RequestClosingEventCode))
            {
                return new LinearRequestClosing();
            }
            return new LinearSchedulingReschedule();
        }


        public LinearClosingActionEventFactory(string closingCompletedEventCode, string receiveFundingEventCode, string notaryDocumentsPickedUpEventCode, string requestClosingEventCode, string schedulingRescheduleEventCode) : base(closingCompletedEventCode, receiveFundingEventCode, notaryDocumentsPickedUpEventCode, requestClosingEventCode, schedulingRescheduleEventCode)
        {
        }
    }
}
