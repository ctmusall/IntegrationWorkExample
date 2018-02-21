using OrderPlacement.Models;

namespace OrderPlacement.Parsers
{
    public interface IOrderParser
    {
        Order ParseOrder(OrderResult orderResult);
    }
}
