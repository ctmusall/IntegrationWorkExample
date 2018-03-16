using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal class AssignedClosingAttorneyStatusDocumentBuilder : AssignedAttorneyStatusDocumentBuilder
    {
        protected internal override void AddClosingDueDateTime(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Closing Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingDate} {eClosingOrder.Order.ClosingTime}");
            documentBuilder.EndRow();
        }

        protected internal override void DetermineAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            AddAttorneyInfo(documentBuilder, eClosingOrder.Order.ClosingAttorney);
        }
    }
}
