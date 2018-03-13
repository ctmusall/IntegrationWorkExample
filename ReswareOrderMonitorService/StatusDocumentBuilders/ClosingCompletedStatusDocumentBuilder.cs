using Aspose.Words;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal class ClosingCompletedStatusDocumentBuilder : StatusDocumentBuilder
    {
        protected internal override void AddBody(DocumentBuilder documentBuilder, OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.Font.Size = 16;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Underline = Underline.Single;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.Writeln("POST CLOSING CLIENT NOTIFICATION");

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.Font.Size = 12;
            documentBuilder.Font.Bold = true;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.Writeln("This file has closed as scheduled.");

            documentBuilder.Writeln();

            documentBuilder.StartTable();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Order/Loan Number");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.OrderId}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.Borrower.FirstName} {eClosingOrder.Order.Borrower.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Co-Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.CoBorrower.FirstName} {eClosingOrder.Order.CoBorrower.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Closing Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingDate} {eClosingOrder.Order.ClosingTime}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Tracking Number");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;

            eClosingOrder.Order.Couriers.ForEach(courier =>
            {
                documentBuilder.Writeln($"{courier.Name} - {courier.TrackingNumber}");
            });

            documentBuilder.EndRow();
            documentBuilder.EndTable();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Writeln();

            AddFeeSchedule(documentBuilder, eClosingOrder);

            documentBuilder.Writeln();
        }
    }
}
