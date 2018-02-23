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
        private readonly IActionEventParser _actionEventParser;
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        internal IEnumerable<ActionEventServiceResult> ActionEvents => _receiveActionEventServiceClient.GetAllActionEvents().Where(actionEvent => !actionEvent.ActionCompleted && actionEvent.ActionCompletedDateTime == null);

        internal ActionEventReader() : this(ReswareOrderDependencyFactory.Resolve<ReceiveActionEventServiceClient>(), ReswareOrderDependencyFactory.Resolve<IActionEventParser>()) { }

        internal ActionEventReader(ReceiveActionEventServiceClient receiveActionEventServiceClient, IActionEventParser actionEventParser)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
            _actionEventParser = actionEventParser;
        }

        public bool CompleteAction(OrderResult order)
        {
            try
            {
                var actionEvents = ActionEvents.Where(actionEvent => string.Equals(actionEvent.FileNumber,order.FileNumber, StringComparison.CurrentCultureIgnoreCase));

                actionEvents.ForEach(actionEvent =>
                {
                    var result = _actionEventParser.ParseActionEvent(order.CustomerId).ResolveActionEvent(actionEvent)
                        .PerformAction(order);
                    if (!result) return;
                    actionEvent.ActionCompleted = true;
                    actionEvent.ActionCompletedDateTime = DateTime.Now;
                    _receiveActionEventServiceClient.UpdateActionEvent(actionEvent);
                });

                return true;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
                return false;
            }
        }
    }
}
