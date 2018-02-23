using System.Collections.Generic;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.ReswareActionEvent;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal abstract class ActionEventReader : IActionEventReader
    {
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        internal IEnumerable<ActionEventServiceResult> ActionEvents => _receiveActionEventServiceClient.GetAllActionEvents().Where(actionEvent => !actionEvent.ActionCompleted && actionEvent.ActionCompletedDateTime == null);

        internal ActionEventReader() : this(ReswareOrderDependencyFactory.Resolve<ReceiveActionEventServiceClient>()) { }

        internal ActionEventReader(ReceiveActionEventServiceClient receiveActionEventServiceClient)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
        }

        public abstract bool CompleteAction(OrderResult order);
    }
}
