using System.Linq;
using Aspose.Words;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Utilities
{
    internal abstract class AssignedAttorneyStatusDocumentUtility : StatusDocumentUtility
    {
        protected internal override void AddBody(DocumentBuilder documentBuilder, OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.Font.Size = 16;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Underline = Underline.Single;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            documentBuilder.Writeln("PCN Network Services Confirmation");

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 10;
            documentBuilder.Font.Bold = true;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            documentBuilder.Writeln($"PC Law Associated Ltd will handle the services requested for the {eClosingOrder.Order.Borrower.FirstName} {eClosingOrder.Order.Borrower.LastName} file:");

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.StartTable();

            documentBuilder.InsertCell();
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
            
            AddClosingDueDateTime(documentBuilder, eClosingOrder);

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Closing Location");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingLocation}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Documents to Attorney");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.DeliveryMethod}");
            documentBuilder.EndRow();

            AddAttorneyInfo(documentBuilder, eClosingOrder);
            
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Services Performed");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;

            eClosingOrder.Order.ClosingAttorney.Services.Where(service => !ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name)).ForEach(service =>
            {
                documentBuilder.Writeln(service.Name);
            });

            documentBuilder.EndRow();
            documentBuilder.EndTable();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            AddAdditionalAttorneyInfo(documentBuilder, eClosingOrder);
            
            AddFeeSchedule(documentBuilder, eClosingOrder);
        }

        protected internal abstract void AddClosingDueDateTime(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder);

        protected internal abstract void AddAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder);

        protected internal abstract void AddAdditionalAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder);

        protected internal abstract void AddFeeSchedule(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder);
    }
}
