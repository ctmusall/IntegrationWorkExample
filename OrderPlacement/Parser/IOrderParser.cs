using System.Collections.Generic;
using OrderPlacement.Models;

namespace OrderPlacement.Parser
{
    public interface IOrderParser
    {
        ICollection<OrderResult> ParseOrderResults(ICollection<Order> orders);
    }
}
