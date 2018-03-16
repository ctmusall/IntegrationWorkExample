using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Parser
{
    public interface IOrderResultParser
    {
        ICollection<OrderResult> ParseAllOrderResults(ICollection<Order> orders);
        OrderResult ParseOrderResult(Order order);
    }
}
