using System;
using Adeptive.ResWare.Services;
using Resware.Entities.ActionEvents;

namespace ActionEventService.Readers
{
    public class ActionEventReader
    {
        internal ActionEvent ParseInput(ReceiveActionEventData receiveActionEventData)
        {
            return new ActionEvent
            {
                CreatedDateTime = DateTime.Now,
                ActionEventCode = receiveActionEventData?.ActionEventCode,
                FileNumber = receiveActionEventData?.FileNumber
            };
        }
    }
}