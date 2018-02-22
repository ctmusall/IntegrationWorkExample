using System.Collections.Generic;
using System.Linq;
using ActionEventService.Models;

namespace ActionEventService.Parsers
{
    internal class ActionEventResultParser
    {
        internal ICollection<ActionEventServiceResult> ParseAllActionEventResults(ICollection<ActionEvent> actionEvents)
        {
            return actionEvents.Select(actionEvent => new ActionEventServiceResult
            {
                Id = actionEvent.Id,
                FileNumber = actionEvent.FileNumber,
                ActionEventCode = actionEvent.ActionEventCode,
                CreatedDateTime = actionEvent.CreatedDateTime,
                ActionCompleted = actionEvent.ActionCompleted,
                ActionCompletedDateTime = actionEvent.ActionCompletedDateTime
            }).ToList();
        }

        internal ActionEventServiceResult ParseActionEventResult(ActionEvent actionEvent)
        {
            return new ActionEventServiceResult
            {
                Id = actionEvent.Id,
                FileNumber = actionEvent.FileNumber,
                ActionEventCode = actionEvent.ActionEventCode,
                CreatedDateTime = actionEvent.CreatedDateTime,
                ActionCompleted = actionEvent.ActionCompleted,
                ActionCompletedDateTime = actionEvent.ActionCompletedDateTime
            };
        }
    }
}