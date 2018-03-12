using System.Linq;
using Aspose.Words;
using ReswareOrderMonitorService.Common;
using ReswareOrderMonitorService.eClosingIntegrationService;
using Unity.Interception.Utilities;

namespace ReswareOrderMonitorService.Utilities
{
    internal class AssignedClosingAttorneyStatusDocumentUtility : AssignedAttorneyStatusDocumentUtility
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

        protected internal override void AddAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
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
            documentBuilder.Write($"{eClosingOrder.Order.ClosingAttorney.Address.Address1} \n {eClosingOrder.Order.ClosingAttorney.Address.City}, {eClosingOrder.Order.ClosingAttorney.Address.State} {eClosingOrder.Order.ClosingAttorney.Address.ZipCode}");
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
        }

        protected internal override void AddAdditionalAttorneyInfo(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
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
            documentBuilder.Write($"{additionalServiceAttorney.Address.Address1} \n {additionalServiceAttorney.Address.Address2} \n {additionalServiceAttorney.Address.Address3} \n {additionalServiceAttorney.Address.City}, {additionalServiceAttorney.Address.State} {additionalServiceAttorney.Address.ZipCode}");
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

            documentBuilder.InsertCell();
            documentBuilder.Font.Bold = true;
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

        protected internal override void AddFeeSchedule(DocumentBuilder documentBuilder, GetOrderResult eClosingOrder)
        {
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
        }
    }
}
