using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Repositories
{
    public interface IReswareOrderRepository
    {
        int SaveReaderResult(ReaderResult readerResult);
        List<Order> GetAllOrders();

    }
}
