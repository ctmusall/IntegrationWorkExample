using System.Linq;
using Aspose.Words;
using Resware.Entities.Orders;
using ReswareCommon.Constants;

namespace Resware.Core.Builders.StatusDocument.AssignedAttorney
{
    public class AssignedAttorneyStatusDocumentBuilder : StatusDocumentBuilder
    {
        protected internal override void AddBody(DocumentBuilder documentBuilder, Order reswareOrder, eClosings.Entities.Orders.Order eClosingOrder)
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
            documentBuilder.Writeln($"Associates LLC will handle the services requested for the {eClosingOrder.Borrower.FirstName} {eClosingOrder.Borrower.LastName} file:");

            documentBuilder.Writeln();

            documentBuilder.ParagraphFormat.ClearFormatting();
            documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            documentBuilder.StartTable();

            documentBuilder.InsertCell();
            documentBuilder.Write("Order/Loan Number");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.OrderId}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.Borrower?.FirstName} {eClosingOrder.Borrower?.LastName}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Co-Borrower Name");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.CoBorrower?.FirstName} {eClosingOrder.CoBorrower?.LastName}");
            documentBuilder.EndRow();
            
            AddClosingDueDateTime(documentBuilder, eClosingOrder);

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Closing Location");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.ClosingLocation}");
            documentBuilder.EndRow();

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Documents to Attorney");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.DeliveryMethod}");
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
        protected internal virtual void AddClosingDueDateTime(DocumentBuilder documentBuilder, eClosings.Entities.Orders.Order eClosingOrder)
        {
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
            documentBuilder.Write("Due Date & Time");
            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = false;
            documentBuilder.Write($"{eClosingOrder.ClosingDate} {eClosingOrder.ClosingTime}");
        }
        protected internal virtual void DetermineAttorneyInfo(DocumentBuilder documentBuilder, eClosings.Entities.Orders.Order eClosingOrder)
        {
            var attorney = eClosingOrder.Attorneys.FirstOrDefault(att => att.Services.Any(service => ServiceNameConstants.TitleOpinionAndDocPrepServices.Contains(service.Name)));
            if (attorney == null) return;
            AddAttorneyInfo(documentBuilder, attorney);
        }
        protected internal void AddAdditionalAttorneyInfo(DocumentBuilder documentBuilder, eClosings.Entities.Orders.Order eClosingOrder)
        {
            var additionalServiceAttorneys = eClosingOrder.Attorneys.Where(att => att.Services.Any(service => ServiceNameConstants.AdditionalAttorneyServices.Contains(service.Name))).ToList();
            if (additionalServiceAttorneys.Count == 0) return;

            var additionalServiceAttorney = additionalServiceAttorneys.First();

            documentBuilder.Font.Bold = true;
            documentBuilder.Font.Name = "Verdana";
            documentBuilder.Font.Size = 11;

            documentBuilder.Writeln("ADDITIONAL ATTORNEY SERVICES");

            documentBuilder.StartTable();

            AddAttorneyInfo(documentBuilder, additionalServiceAttorney);

            documentBuilder.EndTable();

            documentBuilder.Writeln();
        }
        protected internal void AddAttorneyInfo(DocumentBuilder documentBuilder, eClosings.Entities.Attorneys.Attorney attorney)
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
