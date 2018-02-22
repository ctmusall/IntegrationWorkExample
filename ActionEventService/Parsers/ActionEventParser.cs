using ActionEventService.Models;

namespace ActionEventService.Parsers
{
    public class ActionEventParser
    {
        internal ActionEvent ParseActionEvent(ActionEventServiceResult actionEventServiceResult)
        {
            return new ActionEvent
            {
                Id = actionEventServiceResult.Id,
                FileNumber = actionEventServiceResult.FileNumber,
                ActionEventCode = actionEventServiceResult.ActionEventCode,
                CreatedDateTime = actionEventServiceResult.CreatedDateTime,
                ActionCompleted = actionEventServiceResult.ActionCompleted,
                ActionCompletedDateTime = actionEventServiceResult.ActionCompletedDateTime
            };
        }
    }
}