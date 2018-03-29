using System;
using ActionEventService.Factories;
using ActionEventService.Models;
using ActionEventService.Readers;
using Adeptive.ResWare.Services;
using Resware.Data.ActionEvent.Repository;
using ReswareCommon;
using ReswareCommon.Messages;

namespace ActionEventService.Managers
{
    internal class ActionEventManager : IActionEventManager
    {
        private readonly ActionEventReader _actionEventReader;
        private readonly ActionEventRepository _reswareActionEventRepository;

        public ActionEventManager() : this(DependencyFactory.Resolve<ActionEventReader>(),DependencyFactory.Resolve<ActionEventRepository>()) { }

        public ActionEventManager(ActionEventReader actionEventReader, ActionEventRepository reswareActionEventRepository)
        {
            _actionEventReader = actionEventReader;
            _reswareActionEventRepository = reswareActionEventRepository;
        }


        public ActionEventResult PlaceActionEvent(ReceiveActionEventData receiveActionEventData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receiveActionEventData.ActionEventCode)) return new ActionEventResult { Result = 0, Message = ValidationMessages.ActionEventCodeIsNull };

                if (string.IsNullOrWhiteSpace(receiveActionEventData.FileNumber)) return new ActionEventResult { Result = 0, Message = ValidationMessages.FileNumberIsNull };

                var actionEventReaderResult = _actionEventReader.ParseInput(receiveActionEventData);
                return new ActionEventResult
                {
                    Result = _reswareActionEventRepository.SaveNewActionEvent(actionEventReaderResult)
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