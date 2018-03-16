using System;
using System.Collections.Generic;
using ActionEventService.Models;

namespace ActionEventService.Managers
{
    public interface IActionEventServiceResultManager
    {
        ICollection<ActionEventServiceResult> GetAllActionEvents();
        ActionEventServiceResult GetActionEventById(Guid id);
        int DeleteActionEventById(Guid id);
        int UpdateActionEvent(ActionEventServiceResult actionEventServiceResult);
    }
}
