using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Managers
{
    public interface IOrderResultManager
    {
        ICollection<OrderResult> GetAllOrders();
    }
}
