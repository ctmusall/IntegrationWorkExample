using ActionEventService.Data;
using ActionEventService.Factories;
using ActionEventService.Models;

namespace ActionEventService.Repositories
{
    public class ReswareActionEventRepository : IReswareActionEventRepository
    {
        private readonly ReswareActionEventContext _reswareActionEventContext;

        public ReswareActionEventRepository() : this(ActionEventDependencyFactory.Resolve<ReswareActionEventContext>())
        {
            
        }

        public ReswareActionEventRepository(ReswareActionEventContext reswareActionEventContext)
        {
            _reswareActionEventContext = reswareActionEventContext;
        }

        public int SaveReaderResult(ActionEventReaderResult actionEventReaderResult)
        {
            _reswareActionEventContext.ActionEvents.Add(actionEventReaderResult.ActionEvent);

            return _reswareActionEventContext.SaveChanges();
        }
    }
}