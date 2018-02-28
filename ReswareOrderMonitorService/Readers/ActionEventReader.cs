using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Parsers;
using ReswareOrderMonitorService.ReswareActionEvent;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Readers
{
    internal class ActionEventReader : IActionEventReader
    {
        private readonly IActionEventFactoryParser _actionEventParser;
        private readonly IReceiveActionEventService _receiveActionEventServiceClient;

        internal IEnumerable<ActionEventServiceResult> ActionEvents => _receiveActionEventServiceClient.GetAllActionEvents().Where(actionEvent => !actionEvent.ActionCompleted && actionEvent.ActionCompletedDateTime == null);

        internal ActionEventReader() : this(ReswareOrderDependencyFactory.Resolve<IReceiveActionEventService>(), ReswareOrderDependencyFactory.Resolve<IActionEventFactoryParser>()) { }

        internal ActionEventReader(IReceiveActionEventService receiveActionEventServiceClient, IActionEventFactoryParser actionEventParser)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
            _actionEventParser = actionEventParser;
        }

        public void CompleteActions(OrderResult order)
        {
            try
            {
                var actionEvents = ActionEvents.Where(actionEvent => string.Equals(actionEvent.FileNumber, order.FileNumber, StringComparison.CurrentCultureIgnoreCase));

                var actionEventFactory = _actionEventParser.ParseActionEventFactory(order.ClientId);

                actionEvents.ForEach(actionEvent =>
                {
                    var result = actionEventFactory.ResolveActionEvent(actionEvent.ActionEventCode).PerformAction(order);
                    if (!result) return;
                    actionEvent.ActionCompleted = true;
                    actionEvent.ActionCompletedDateTime = DateTime.Now;
                    _receiveActionEventServiceClient.UpdateActionEvent(actionEvent);
                });
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }
    }
}
