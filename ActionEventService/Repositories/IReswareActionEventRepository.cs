using ActionEventService.Models;

namespace ActionEventService.Repositories
{
    public interface IReswareActionEventRepository
    {
        int SaveReaderResult(ActionEventReaderResult actionEventReaderResult);
    }
}
