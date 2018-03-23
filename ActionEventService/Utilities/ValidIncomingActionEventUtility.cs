using ActionEventService.Models;
using Adeptive.ResWare.Services;
using ReswareCommon;

namespace ActionEventService.Utilities
{
    internal class ValidIncomingActionEventUtility
    {
        internal ValidIncomingActionEventResult IsIncomingActionEventDataValid(ReceiveActionEventData receiveActionEventData)
        {
            if (string.IsNullOrWhiteSpace(receiveActionEventData.ActionEventCode)) return new ValidIncomingActionEventResult {Valid = false, Message = ValidationMessages.ActionEventCodeIsNull};

            return string.IsNullOrWhiteSpace(receiveActionEventData.FileNumber) ? 
                new ValidIncomingActionEventResult {Valid = false, Message = ValidationMessages.FileNumberIsNull} : 
                new ValidIncomingActionEventResult {Valid = true};
        }
    }
}