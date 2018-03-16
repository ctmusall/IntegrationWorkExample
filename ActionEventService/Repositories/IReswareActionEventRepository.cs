using System;
using System.Collections.Generic;
using ActionEventService.Models;

namespace ActionEventService.Repositories
{
    public interface IReswareActionEventRepository
    {
        int SaveReaderResult(ActionEventReaderResult actionEventReaderResult);
        List<ActionEvent> GetAllActionEvents();
        ActionEvent GetActionEventById(Guid id);
        int DeleteActionEventById(Guid id);
        int UpdateActionEvent(ActionEvent actionEvent);
    }
}
