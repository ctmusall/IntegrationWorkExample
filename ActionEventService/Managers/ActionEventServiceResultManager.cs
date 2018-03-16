using System;
using System.Collections.Generic;
using ActionEventService.Factories;
using ActionEventService.Models;
using ActionEventService.Parsers;
using ActionEventService.Repositories;

namespace ActionEventService.Managers
{
    internal class ActionEventServiceResultManager : IActionEventServiceResultManager
    {
        private readonly IReswareActionEventRepository _reswareActionEventRepository;
        private readonly ActionEventResultParser _actionEventResultParser;
        private readonly ActionEventParser _actionEventParser;

        public ActionEventServiceResultManager() : this(ActionEventDependencyFactory.Resolve<IReswareActionEventRepository>(), ActionEventDependencyFactory.Resolve<ActionEventResultParser>(), ActionEventDependencyFactory.Resolve<ActionEventParser>()) { }

        internal ActionEventServiceResultManager(IReswareActionEventRepository reswareActionEventRepository, ActionEventResultParser actionEventResultParser, ActionEventParser actionEventParser)
        {
            _reswareActionEventRepository = reswareActionEventRepository;
            _actionEventResultParser = actionEventResultParser;
            _actionEventParser = actionEventParser;
        }

        public ICollection<ActionEventServiceResult> GetAllActionEvents()
        {
            try
            {
                var actionEvents = _reswareActionEventRepository.GetAllActionEvents();
                return _actionEventResultParser.ParseAllActionEventResults(actionEvents);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionEventServiceResult GetActionEventById(Guid id)
        {
            try
            {
                var actionEvent = _reswareActionEventRepository.GetActionEventById(id);
                return _actionEventResultParser.ParseActionEventResult(actionEvent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int DeleteActionEventById(Guid id)
        {
            try
            {
                return _reswareActionEventRepository.DeleteActionEventById(id);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateActionEvent(ActionEventServiceResult actionEventServiceResult)
        {
            try
            {
                var actionEvent = _actionEventParser.ParseActionEvent(actionEventServiceResult);
                return _reswareActionEventRepository.UpdateActionEvent(actionEvent);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}