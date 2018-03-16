using System;
using System.Collections.Generic;
using System.Linq;
using ActionEventService.Data;
using ActionEventService.Factories;
using ActionEventService.Models;

namespace ActionEventService.Repositories
{
    public class ReswareActionEventRepository : IReswareActionEventRepository
    {
        private readonly ReswareActionEventContext _reswareActionEventContext;

        public ReswareActionEventRepository() : this(ActionEventDependencyFactory.Resolve<ReswareActionEventContext>())
        {
            
        }

        public ReswareActionEventRepository(ReswareActionEventContext reswareActionEventContext)
        {
            _reswareActionEventContext = reswareActionEventContext;
        }

        public int SaveReaderResult(ActionEventReaderResult actionEventReaderResult)
        {
            _reswareActionEventContext.ActionEvents.Add(actionEventReaderResult.ActionEvent);

            return _reswareActionEventContext.SaveChanges();
        }

        public List<ActionEvent> GetAllActionEvents()
        {
            return _reswareActionEventContext.ActionEvents.ToList();
        }

        public ActionEvent GetActionEventById(Guid id)
        {
            return _reswareActionEventContext.ActionEvents.FirstOrDefault(a => a.Id == id);
        }

        public int DeleteActionEventById(Guid id)
        {
            var actionEvent = _reswareActionEventContext.ActionEvents.FirstOrDefault(a => a.Id == id);

            if (actionEvent == null) return 0;

            _reswareActionEventContext.ActionEvents.Remove(actionEvent);

            return _reswareActionEventContext.SaveChanges();
        }

        public int UpdateActionEvent(ActionEvent updatedActionEvent)
        {
            var actionEvent = _reswareActionEventContext.ActionEvents.FirstOrDefault(a => a.Id == updatedActionEvent.Id);

            if (actionEvent == null) return 0;

            _reswareActionEventContext.Entry(actionEvent).CurrentValues.SetValues(updatedActionEvent);

            return _reswareActionEventContext.SaveChanges();
        }
    }
}