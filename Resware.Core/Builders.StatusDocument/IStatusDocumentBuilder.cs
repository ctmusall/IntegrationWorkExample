using Aspose.Words;
using Resware.Entities.Orders;

namespace Resware.Core.Builders.StatusDocument
{
    public interface IStatusDocumentBuilder
    {
        Document BuildDocument(Order reswareOrder, eClosings.Entities.Orders.Order eClosingOrder);
    }
}
