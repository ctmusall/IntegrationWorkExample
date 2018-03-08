using System;
using System.Linq;
using ReswareOrderMonitorService.Factories;
using ReswareOrderMonitorService.Factories.ActionEvents;
using ReswareOrderMonitorService.ReswareActionEvent;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.Readers
{
    internal class ActionEventReader : IActionEventReader
    {
        private readonly IParentActionEventFactory _parentActionEventFactory;
        private readonly ReceiveActionEventServiceClient _receiveActionEventServiceClient;

        public ActionEventReader() : this(new ReceiveActionEventServiceClient(), ReswareOrderDependencyFactory.Resolve<IParentActionEventFactory>()) { }

        internal ActionEventReader(ReceiveActionEventServiceClient receiveActionEventServiceClient, IParentActionEventFactory parentActionEventParser)
        {
            _receiveActionEventServiceClient = receiveActionEventServiceClient;
            _parentActionEventFactory = parentActionEventParser;
        }

        public void CompleteActions(OrderResult order)
        {
            try
            {
                var actionEvents = _receiveActionEventServiceClient.GetAllActionEvents()
                    .Where(ae => !ae.ActionCompleted && ae.ActionCompletedDateTime == null 
                    && string.Equals(ae.FileNumber,order.FileNumber, StringComparison.CurrentCultureIgnoreCase)).ToList();

                if (actionEvents.Count == 0) return;

                actionEvents.ForEach(actionEvent =>
                {
                    var result = _parentActionEventFactory.ResolveActionEventFactory(order.ClientId).ResolveActionEvent(actionEvent.ActionEventCode).PerformAction(order);
                    if (!result) return;
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
