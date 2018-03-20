using System.Linq;
using Aspose.Words;
using ReswareCommon;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using ReswareOrderMonitorService.ReswareOrders;

namespace ReswareOrderMonitorService.StatusDocumentBuilders
{
    internal class AssignedAttorneyStatusDocumentBuilder : StatusDocumentBuilder
    {
        protected internal override void AddBody(DocumentBuilder documentBuilder, OrderResult reswareOrder, GetOrderResult eClosingOrder)
        {
            documentBuilder.Font.Name = "Times New Roman";
            documentBuilder.Font.Size = 16;
            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Underline = Underline.Single;

            documentBuilder.Writeln("PCN Network Services Confirmation");

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 10;
            documentBuilder.Font.Bold = true;
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            documentBuilder.Writeln($"Associates LLC will handle the services requested for the {eClosingOrder.Order.Borrower.FirstName} {eClosingOrder.Order.Borrower.LastName} file:");

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

            DetermineAttorneyInfo(documentBuilder, eClosingOrder);

            documentBuilder.EndTable();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.Font.ClearFormatting();

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            AddAdditionalAttorneyInfo(documentBuilder, eClosingOrder);
            
            AddFeeSchedule(documentBuilder, eClosingOrder);
        }
        protected internal virtual void AddClosingDueDateTime(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Due Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Order.ClosingDate} {eClosingOrder.Order.ClosingTime}");
        }
        protected internal virtual void DetermineAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            var attorney = eClosingOrder.Order.Attorneys.FirstOrDefault(att => att.Services.Any(service => ServiceNameConstants.TitleOpinionAndDocPrepServices.Contains(service.Name)));
            if (attorney == null) return;
            AddAttorneyInfo(documentBuilder, attorney);
        }
        protected internal void AddAdditionalAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
            var additionalServiceAttorneys = eClosingOrder.Order.Attorneys.Where(att => att.Services.Any(service => ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name))).ToList();
            if (additionalServiceAttorneys.Count == 0) return;

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
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Address");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.Address.Address1} \n {additionalServiceAttorney.Address.City}, {additionalServiceAttorney.Address.State} {additionalServiceAttorney.Address.ZipCode}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.CellPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Alt. Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.HomePhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Fax");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.FaxNumber1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney E-mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.Email1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Alt. Email");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{additionalServiceAttorney.Email2}");
            documentBuilder.EndRow();

            documentBuilder.EndTable();

            documentBuilder.Writeln();
        }
        protected internal void AddAttorneyInfo(DocumentBuilder documentBuilder, AttorneyInfoForOrder attorney)
        {
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.FirstName} {attorney.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Address");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.Address.Address1} \n {attorney.Address.City}, {attorney.Address.State} {attorney.Address.ZipCode}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Cell Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.CellPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Home Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.HomePhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Work Phone");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.WorkPhone}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Fax");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.FaxNumber1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.Email1}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Attorney Alt. E-Mail");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{attorney.Email2}");
            documentBuilder.EndRow();
        }
    }
}
