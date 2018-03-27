using OrderPlacement.Models;
using Resware.Entities.Orders;

namespace OrderPlacement.Parsers
{
    public interface IOrderParser
    {
        Order ParseOrder(OrderResult orderResult);
    }
}
