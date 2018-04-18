using System.Collections.Generic;
using System.Linq;
using eClosings.Data.eClosingsIntegrationService;
using eClosings.Entities.Attorneys;
using eClosings.Entities.Orders;

namespace eClosings.Data.Readers.OrderReader
{
    internal class EClosingOrderReader
    {
        public Order MapEClosingOrder(GetOrderResult getOrderResult)
        {
            if (getOrderResult?.Outcome == OutcomeEnum.Fail || getOrderResult?.Order == null) return null;

            return new Order
            {
                AdjournedReason = getOrderResult.Order.AdjournedReason,
                Attorneys = MapEClosingAttorneys(getOrderResult.Order.Attorneys),
                BorrowerContactedDateTime = getOrderResult.Order.BorrowerContacted,
                BorrowerContactedStatusSentDateTime = getOrderResult.Order.BorrowerContactedStatusSent,
                CancelledReason = getOrderResult.Order.CancelledReason,
                ClosingAttorney = MapEClosingAttorney(getOrderResult.Order.ClosingAttorney),
                Couriers = MapEClosingCouriers(getOrderResult.Order.Couriers),
                CustomerName = getOrderResult.Order.CustomerName,
                NotClosedReason = getOrderResult.Order.NotClosedReason,
                Notes = getOrderResult.Order.Notes,
                Status = getOrderResult.Order.Status,
                TotalBillRate = getOrderResult.Order.TotalBillRate,
                UnableReason = getOrderResult.Order.UnableReason,
                Borrower = MapEClosingPerson(getOrderResult.Order.Borrower),
                CoBorrower = MapEClosingPerson(getOrderResult.Order.CoBorrower),
                ClosingAddress = MapEClosingAddress(getOrderResult.Order.ClosingAddress),
                ClosingDate = getOrderResult.Order.ClosingDate,
                ClosingLocation = getOrderResult.Order.ClosingLocation,
                ClosingTime = getOrderResult.Order.ClosingTime,
                CustomerId = getOrderResult.Order.CustomerId,
                CustomerContact = getOrderResult.Order.CustomerContact,
                DeliveryMethod = getOrderResult.Order.DeliveryMethod,
                FileNumber = getOrderResult.Order.FileNumber,
                LenderName = getOrderResult.Order.LenderName,
                LoanNumber = getOrderResult.Order.LoanNumber,
                OrderId = getOrderResult.Order.OrderId,
                Product = getOrderResult.Order.Product,
                RequestedClosingDate = getOrderResult.Order.RequestedClosingDate,
                RequestedClosingTime = getOrderResult.Order.RequestedClosingTime
            };
        }

        private static Attorney MapEClosingAttorney(AttorneyInfoForOrder att)
        {
            if (att == null) return null;
            return new Attorney
            {
                Address = MapEClosingAddress(att.Address),
                AttorneyId = att.AttorneyId,
                CellPhone = att.CellPhone,
                Email1 = att.Email1,
                Email2 = att.Email2,
                FaxNumber1 = att.FaxNumber1,
                FaxNumber2 = att.FaxNumber2,
                FirstName = att.FirstName,
                HomePhone = att.HomePhone,
                LastName = att.LastName,
                Services = MapEClosingServices(att.Services),
                MiddleName = att.MiddleName,
                Salutation = att.Salutation,
                Suffix = att.SuffixName,
                WorkPhone = att.WorkPhone
            };
        }

        private static ICollection<Attorney> MapEClosingAttorneys(IEnumerable<AttorneyInfoForOrder> attorneys)
        {
            return attorneys?.Select(MapEClosingAttorney).ToList();
        }

        private static Entities.Addresses.Address MapEClosingAddress(Address address)
        {
            if (address == null) return null;
            return new Entities.Addresses.Address
            {
                Address1 = address.Address1,
                Address2 = address.Address2,
                Address3 = address.Address3,
                City = address.City,
                State = address.State,
                County = address.County,
                ZipCode = address.ZipCode
            };
        }

        private static ICollection<Entities.Services.Service> MapEClosingServices(IEnumerable<Service> services)
        {
            return services?.Select(svc => new Entities.Services.Service
            {
                BillRate = svc.BillRate,
                Name = svc.Name,
                PayRate = svc.PayRate
            }).ToList();
        }

        private static ICollection<Entities.Couriers.Courier> MapEClosingCouriers(IEnumerable<Courier> couriers)
        {
            return couriers?.Select(c => new Entities.Couriers.Courier
            {
                Name = c.Name,
                TrackingNumber = c.TrackingNumber
            }).ToList();
        }

        private static Entities.Persons.Person MapEClosingPerson(Person person)
        {
            if (person == null) return null;
            return new Entities.Persons.Person
            {
                Address = MapEClosingAddress(person.Address),
                CellPhone = person.CellPhone,
                Email1 = person.Email1,
                Email2 = person.Email2,
                FaxNumber1 = person.FaxNumber1,
                FaxNumber2 = person.FaxNumber2,
                FirstName = person.FirstName,
                HomePhone = person.HomePhone,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Salutation = person.Salutation,
                Suffix = person.SuffixName,
                WorkPhone = person.WorkPhone
            };
        }
    }

}
