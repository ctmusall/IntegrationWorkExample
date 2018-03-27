using System.Collections.Generic;
using OrderPlacement.Models;
using Resware.Entities.Orders;

namespace OrderPlacement.Parsers
{
    public interface IOrderResultParser
    {
        ICollection<OrderResult> ParseAllOrderResults(ICollection<Order> orders);
        OrderResult ParseOrderResult(Order order);
    }
}
