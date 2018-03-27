using System;
using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Managers
{
    public interface IOrderResultManager
    {
        ICollection<OrderResult> GetAllOrders();
        int UpdateOrder(OrderResult order);
    }
}
