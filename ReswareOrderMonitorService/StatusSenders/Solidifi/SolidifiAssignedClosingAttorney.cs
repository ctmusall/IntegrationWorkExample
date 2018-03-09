using System;
using System.Linq;
using Aspose.Words;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.StatusSenders.Solidifi
{
    internal class SolidifiAssignedClosingAttorney : SolidifiStatusSender
    {
        internal SolidifiAssignedClosingAttorney(GetOrderResult eClosingOrder) : base(eClosingOrder) { }

        protected internal override void UpdateReswareOrderStatus(OrderResult reswareOrder)
        {
            reswareOrder.ClosingStatus = EClosingOrder.Order.Status;
            OrderPlacementServiceClient.UpdateOrder(reswareOrder);
        }

        protected internal override bool SendDocumentToResware()
        {
            throw new System.NotImplementedException();
        }

        protected internal override void BuildStatusUpdateDocument(OrderResult reswareOrder)
        {
            var document = new Document();

            var documentBuilder = new DocumentBuilder(document);

            documentBuilder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Hidden = false;
            documentBuilder.Font.Size = 12;
            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            documentBuilder.Write($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");

            documentBuilder.MoveToDocumentStart();
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.Font.Size = 16;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Underline = Underline.Single;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            documentBuilder.Writeln("PCN Network Services Confirmation");

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 10;
            documentBuilder.Font.Bold = true;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            documentBuilder.Writeln($"PC Law Associated Ltd will handle the services requested for the {EClosingOrder.Order.Borrower.FirstName} {EClosingOrder.Order.Borrower.MiddleName} {EClosingOrder.Order.Borrower.LastName} file:");

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.StartTable();

            documentBuilder.InsertCell();
            documentBuilder.Write("Order / Loan Number");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.LoanNumber}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.Borrower.FirstName} {EClosingOrder.Order.Borrower.MiddleName} {EClosingOrder.Order.Borrower.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Co-Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.CoBorrower.FirstName} {EClosingOrder.Order.CoBorrower.MiddleName} {EClosingOrder.Order.CoBorrower.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Closing Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingDate} {EClosingOrder.Order.ClosingTime}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Closing Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingDate} {EClosingOrder.Order.ClosingTime}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Closing Location");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingLocation}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Documents to Attorney");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.DeliveryMethod}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.FirstName} {EClosingOrder.Order.ClosingAttorney.MiddleName} {EClosingOrder.Order.ClosingAttorney.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Address");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.Address.Address1} \n {EClosingOrder.Order.ClosingAttorney.Address.City}, {EClosingOrder.Order.ClosingAttorney.Address.State} {EClosingOrder.Order.ClosingAttorney.Address.ZipCode}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Cell Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.CellPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Home Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.HomePhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Work Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.WorkPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Fax");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.FaxNumber1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.Email1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Attorney Alt. E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{EClosingOrder.Order.ClosingAttorney.Email2}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Write("Services Performed");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;

            EClosingOrder.Order.ClosingAttorney.Services.Where(service => !ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name)).ForEach(service =>
            {
                documentBuilder.Writeln(service.Name);
            });

            documentBuilder.EndRow();
            documentBuilder.EndTable();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();


            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            if (EClosingOrder.Order.Attorneys.Any(att => att.Services.Any(service => ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name))))
            {
                documentBuilder.Font.Bold = true;
                documentBuilder.Font.Name = "Verdana";
                documentBuilder.Font.Size = 11;

                documentBuilder.Writeln("ADDITIONAL ATTORNEY SERVICES");

            }
            
            
        }
    }
}
