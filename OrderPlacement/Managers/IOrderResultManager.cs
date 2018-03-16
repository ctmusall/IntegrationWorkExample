using System;
using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Managers
{
    public interface IOrderResultManager
    {
        ICollection<OrderResult> GetAllOrders();
        OrderResult GetOrderById(Guid id);
        int DeleteOrderById(Guid id);
        int UpdateOrder(OrderResult order);
    }
}
