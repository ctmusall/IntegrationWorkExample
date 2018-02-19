using OrderPlacement.Models;
using OrderPlacement.Readers;

namespace OrderPlacement.Repositories
{
    public interface IReswareOrderRepository
    {
        int SaveReaderResult(ReaderResult readerResult);
    }
}
