using System;
using ActionEventService.Models;
using Adeptive.ResWare.Services;

namespace ActionEventService
{
    public class ActionEventReader
    {
        internal ActionEventReaderResult ParseInput(ReceiveActionEventData receiveActionEventData)
        {
            return new ActionEventReaderResult
            {
                ActionEvent = MapActionEvent(receiveActionEventData)
            };
        }

        private static ActionEvent MapActionEvent(ReceiveActionEventData receiveActionEventData)
        {
            return new ActionEvent
            {
                CreatedDateTime = DateTime.Now,
                ActionEventCode = receiveActionEventData.ActionEventCode,
                FileNumber = receiveActionEventData.FileNumber
            };
        }
    }
}