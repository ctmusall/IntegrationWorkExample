using System;
using ActionEventService.Factories;
using ActionEventService.Models;
using ActionEventService.Repositories;
using Adeptive.ResWare.Services;

namespace ActionEventService.Managers
{
    public class ActionEventManager : IActionEventManager
    {
        private readonly ActionEventReader _actionEventReader;
        private readonly IReswareActionEventRepository _reswareActionEventRepository;

        public ActionEventManager() : this(ActionEventDependencyFactory.Resolve<ActionEventReader>(),ActionEventDependencyFactory.Resolve<IReswareActionEventRepository>())
        {
            
        }

        public ActionEventManager(ActionEventReader actionEventReader, IReswareActionEventRepository reswareActionEventRepository)
        {
            _actionEventReader = actionEventReader;
            _reswareActionEventRepository = reswareActionEventRepository;
        }


        public ActionEventResult PlaceActionEvent(ReceiveActionEventData receiveActionEventData)
        {
            try
            {
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