using System;
using ActionEventService.Factories;
using ActionEventService.Models;
using ActionEventService.Repositories;
using ActionEventService.Utilities;
using Adeptive.ResWare.Services;

namespace ActionEventService.Managers
{
    internal class ActionEventManager : IActionEventManager
    {
        private readonly ActionEventReader _actionEventReader;
        private readonly IReswareActionEventRepository _reswareActionEventRepository;
        private readonly ValidIncomingActionEventUtility _validIncomingActionEventUtility;

        public ActionEventManager() : this(ActionEventDependencyFactory.Resolve<ActionEventReader>(),ActionEventDependencyFactory.Resolve<IReswareActionEventRepository>(), ActionEventDependencyFactory.Resolve<ValidIncomingActionEventUtility>())
        {
            
        }

        public ActionEventManager(ActionEventReader actionEventReader, IReswareActionEventRepository reswareActionEventRepository, ValidIncomingActionEventUtility validIncomingActionEventUtility)
        {
            _actionEventReader = actionEventReader;
            _reswareActionEventRepository = reswareActionEventRepository;
            _validIncomingActionEventUtility = validIncomingActionEventUtility;
        }


        public ActionEventResult PlaceActionEvent(ReceiveActionEventData receiveActionEventData)
        {
            try
            {
                var validIncomingActionEvent = _validIncomingActionEventUtility.IsIncomingActionEventDataValid(receiveActionEventData);

                if (!validIncomingActionEvent.Valid) return new ActionEventResult {Result = 0, Message = validIncomingActionEvent.Message};

                var actionEventReaderResult = _actionEventReader.ParseInput(receiveActionEventData);
                return new ActionEventResult
                {
                    Result = _reswareActionEventRepository.SaveReaderResult(actionEventReaderResult)
                };
            }
            catch (Exception ex)
            {
                return new ActionEventResult
                {
                    Result = -1,
                    Message = $"ERROR! Message: {ex.Message} \n\n Inner Exception: {ex.InnerException} \n\n Stack Trace: {ex.StackTrace}"
                };
            }
        }
    }
}