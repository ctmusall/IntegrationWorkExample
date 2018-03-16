using System;
using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Repositories
{
    public interface IReswareOrderRepository
    {
        int SaveReaderResult(ReaderResult readerResult);
        List<Order> GetAllOrders();
        Order GetOrderById(Guid id);
        int DeleteOrderById(Guid id);
        int UpdateOrder(Order updatedOrder);
    }
}
