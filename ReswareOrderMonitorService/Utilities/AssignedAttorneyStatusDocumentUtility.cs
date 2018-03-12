using System;
using System.Linq;
using Aspose.Words;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Utilities
{
    internal class AssignedAttorneyStatusDocumentUtility : StatusDocumentUtility
    {
        public override Document BuildDocument(OrderResult reswareOrder, GetOrderResult eClosingOrder)
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
            documentBuilder.Write("Order / Loan Number");
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

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.FirstName} {eClosingOrder.Order.ClosingAttorney.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Address");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.Address.Address1} \n {eClosingOrder.Order.ClosingAttorney.Address.Address2} \n {eClosingOrder.Order.ClosingAttorney.Address.Address3} \n {eClosingOrder.Order.ClosingAttorney.Address.City}, {eClosingOrder.Order.ClosingAttorney.Address.State} {eClosingOrder.Order.ClosingAttorney.Address.ZipCode}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Cell Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.CellPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Home Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.HomePhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Work Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.WorkPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Fax");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.FaxNumber1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.Email1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Alt. E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.Email2}");
            documentBuilder.EndRow();

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

            var additionalServiceAttorneys = eClosingOrder.Order.Attorneys.Where(att => att.Services.Any(service => ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name))).ToList();
            if (additionalServiceAttorneys.Count != 0)
            {
                var additionalServiceAttorney = additionalServiceAttorneys.First();

                documentBuilder.Font.Bold = true;
                documentBuilder.Font.Name = "Verdana";
                documentBuilder.Font.Size = 11;

                documentBuilder.Writeln("ADDITIONAL ATTORNEY SERVICES");

                documentBuilder.StartTable();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Name");
                documentBuilder.InsertCell();
                documentBuilder.Write($"{additionalServiceAttorney.FirstName} {additionalServiceAttorney.LastName}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Address");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.Address.Address1} \n {additionalServiceAttorney.Address.Address2} \n {additionalServiceAttorney.Address.Address3} \n {additionalServiceAttorney.Address.City}, {additionalServiceAttorney.Address.State} {additionalServiceAttorney.Address.ZipCode}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Phone");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.CellPhone}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Alt. Phone");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.HomePhone}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Fax");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.FaxNumber1}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney E-mail");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.Email1}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Attorney Alt. Email");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;
                documentBuilder.Write($"{additionalServiceAttorney.Email2}");
                documentBuilder.EndRow();

                documentBuilder.InsertCell();
                documentBuilder.Write("Services Performed");
                documentBuilder.InsertCell();
                documentBuilder.Font.Bold = false;

                additionalServiceAttorney.Services.ForEach(service =>
                {
                    documentBuilder.Writeln(service.Name);
                });

                documentBuilder.EndRow();
                documentBuilder.EndTable();

                documentBuilder.Writeln();
            }

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();


            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 11;

            documentBuilder.Writeln("FEE SCHEDULE");

            documentBuilder.Font.Size = 10;

            documentBuilder.StartTable();

            documentBuilder.InsertCell();
            documentBuilder.Write("Total Fee");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;

            documentBuilder.Write($"{eClosingOrder.Order.TotalBillRate}");
            documentBuilder.EndRow();

            documentBuilder.EndTable();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Size = 10;
            documentBuilder.Font.Name = "Verdana";
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;

            documentBuilder.Writeln();

            documentBuilder.Writeln("Thank you for the order.");

            documentBuilder.Writeln();

            documentBuilder.Writeln("PCN Network");
            documentBuilder.Writeln("Administrative Agent for PC Law Associates Ltd");
            documentBuilder.Writeln("200 Fleet Street, Suite 1100");
            documentBuilder.Writeln("Pittsburgh, PA  15220");
            documentBuilder.Writeln("Fax - 412-928-2459");
            documentBuilder.Writeln("Main Line - 412-928-2450");
            documentBuilder.Writeln("Scheduling	- 412-928-2782");
            documentBuilder.Writeln("Docs/Post-Closing - 412-928-2783");
            documentBuilder.Writeln("Disbursements - 412-928-2784");
            documentBuilder.Writeln("Accounting - 412-928-2788");

            documentBuilder.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);
            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Hidden = false;
            documentBuilder.Font.Name = "Courier New";
            documentBuilder.Font.Size = 9;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.Writeln("PC Law Associates Ltd");
            documentBuilder.Writeln("Main Office: 200 Fleet Street, Suite 1100, Pittsburgh, PA 15220, Phone: 866-583-091");
            documentBuilder.Writeln("Delaware Office: 42 Reads Way, New Castle, DE 19720, Phone: 302-327-8084");
            documentBuilder.Writeln("South Carolina Office: 226 State Street, West Columbia, SC, Phone: 803-873-9198");

            return document;
        }
    }
}
