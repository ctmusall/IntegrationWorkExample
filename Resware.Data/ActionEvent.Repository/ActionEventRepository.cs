using System.Collections.Generic;
using System.Linq;
using Resware.Data.Repository;

namespace Resware.Data.ActionEvent.Repository
{
    public class ActionEventRepository : RepositoryBase
    {
        public int SaveNewActionEvent(Entities.ActionEvents.ActionEvent actionEvent)
        {
            if (actionEvent == null) return -1;

            ReswareDbContext.ActionEvents.Add(actionEvent);

            return ReswareDbContext.SaveChanges();
        }

        public List<Entities.ActionEvents.ActionEvent> GetAllActionEvents()
        {
            return ReswareDbContext.ActionEvents.ToList();
        }

        public int UpdateActionEvent(Entities.ActionEvents.ActionEvent updatedActionEvent)
        {
            var actionEvent = ReswareDbContext.ActionEvents.FirstOrDefault(a => a.Id == updatedActionEvent.Id);

            if (actionEvent == null) return 0;

            ReswareDbContext.Entry(actionEvent).CurrentValues.SetValues(updatedActionEvent);

            return ReswareDbContext.SaveChanges();
        }
    }
}