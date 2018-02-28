﻿using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Parsers;
using ReswareOrderMonitorService.ReswareActionEvent;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal class ActionEventReader : IActionEventReader
    {
        private readonly IActionEventFactoryParser _actionEventParser;
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        public ActionEventReader() : this(new ReceiveActionEventServiceClient(), ReswareOrderDependencyFactory.Resolve<IActionEventFactoryParser>()) { }

        internal ActionEventReader(ReceiveActionEventServiceClient receiveActionEventServiceClient, IActionEventFactoryParser actionEventParser)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
            _actionEventParser = actionEventParser;
        }

        public void CompleteActions(OrderResult order)
        {
            try
            {
                var actionEvents = _receiveActionEventServiceClient.GetAllActionEvents()
                    .Where(ae => !ae.ActionCompleted && ae.ActionCompletedDateTime == null && string.Equals(ae.FileNumber,
                        order.FileNumber, StringComparison.CurrentCultureIgnoreCase)).ToList();

                if (actionEvents.Any()) return;

                var actionEventFactory = _actionEventParser.ParseActionEventFactory(order.ClientId);

                actionEvents.ForEach(actionEvent =>
                {
                    var result = actionEventFactory?.ResolveActionEvent(actionEvent.ActionEventCode)?.PerformAction(order);
                    if (result == null || result.Value == false) return;
                    actionEvent.ActionCompleted = true;
                    actionEvent.ActionCompletedDateTime = DateTime.Now;
                    _receiveActionEventServiceClient.UpdateActionEvent(actionEvent);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
