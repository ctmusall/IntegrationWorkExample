using System;
using System.Diagnostics;
using System.Linq;
using Resware.Core.ActionEvent.Factories;
using Resware.Core.ActionEvent.Factories.ParentActionEvents;
using Resware.Data.ActionEvent.Repository;

namespace Resware.Core.ActionEvent.Readers.ActionEvents
{
    public class ActionEventReader
    {
        private readonly IParentActionEventFactory _parentActionEventFactory;
        private readonly ActionEventRepository _receiveActionEventRepository;

        public ActionEventReader() : this(DependencyFactory.Resolve<ActionEventRepository>(), DependencyFactory.Resolve<IParentActionEventFactory>()) { }

        internal ActionEventReader(ActionEventRepository receiveActionEventRepository, IParentActionEventFactory parentActionEventParser)
        {
            _receiveActionEventRepository = receiveActionEventRepository;
            _parentActionEventFactory = parentActionEventParser;
        }

        public void CompleteActions(Entities.Orders.Order order)
        {
            try
            {
                if (order == null) return;

                var actionEvents = _receiveActionEventRepository.GetAllActionEvents()
                    .Where(ae => !ae.ActionCompleted && ae.ActionCompletedDateTime == null 
                    && string.Equals(ae.FileNumber,order.FileNumber, StringComparison.CurrentCultureIgnoreCase)).ToList();

                if (actionEvents.Count == 0) return;

                actionEvents.ForEach(actionEvent =>
                {
                    var result = _parentActionEventFactory.ResolveActionEventFactory(order.ClientId)?.ResolveActionEvent(actionEvent.ActionEventCode)?.PerformAction(order);
                    if (!result.HasValue) return;
                    actionEvent.ActionCompleted = true;
                    actionEvent.ActionCompletedDateTime = DateTime.Now;
                    _receiveActionEventRepository.UpdateActionEvent(actionEvent);
                });
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
