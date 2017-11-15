﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using PCN_Integration.DataModels;
using PCN_Integration.Services.Common;
using PCN_Integration.Services.Models;
using PCN_Integration.Services.PcnIntegrationServiceTest;
using PCN_Integration.Services.Utilities;

namespace PCN_Integration.Services.Services
{
    public class FassMonitor : IntegrationServiceBase
    {
        private static void TrackNewFassOrders()
        {
            var orders = GetRecentOrdersFromPcn();
            if (orders.Count <= 0) return;

            foreach (var order in orders)
            {
                var fassOrder = new FassOrder
                {
                    OrderId = order.ORDERID,
                    FileNo = order.FILE_NUM,           
                    DateTimeLastUpdated = order.CREATE_DATE,
                    Status = ConvertStatusIdToString(order.STATUS),
                    StatusLastUpdated = ConvertStatusIdToString(order.STATUS),
                    DateTimeCreated = order.CREATE_DATE,
                    CustomerId = order.CUSTOMERID
                };                   
                AddOrderToMonitorDb(fassOrder);
            }
        }

        private static void SendFassNotificationOnUpdate()
        {     
            var trackedFassOrders = GetTrackedOrders();
            var trackedFassOrderIds = trackedFassOrders.Select(fassOrder => fassOrder.OrderId).ToList();

            var pcnOrders = GetOrdersFromPcnById(trackedFassOrderIds);

            foreach (var trackedOrder in trackedFassOrders)
            {
                BuildFassResponseMessageAndSendUpdate(pcnOrders, trackedOrder);
            }
        }

        private static void BuildFassResponseMessageAndSendUpdate(IEnumerable<OSGPCN300> pcnOrders, FassOrder trackedOrder)
        {
            var pcnOrder = pcnOrders.FirstOrDefault(o =>
                string.Equals(o.ORDERID, trackedOrder.OrderId, StringComparison.InvariantCultureIgnoreCase));

            if (pcnOrder == null) return;

            if (trackedOrder.Status == ConvertStatusIdToString(pcnOrder.STATUS)) return;

            var pcnWebService = new PcnWebServiceInvoker();
            bool success;

            var response = pcnWebService.GetOrderFromService(pcnOrder.CUSTOMERID, pcnOrder.ORDERID, out success);
            
            if (IsNotMostRecentOrder(response)) return;

            var order = response.GetOrderResult.Order;
                        
            var fassMessage = new FassMonitorResponseMessage
            {
                OrderId = order.OrderId,
                OrderStatus = order.Status,
                Notes = GetNote(order)
            };

            if (order.ClosingAttorney != null) SetFassClosingAttorney(fassMessage, order);

            if (FassOrderIsUnableToFill(fassMessage, order.UnableReason)) SetFassUnableToFillCodeAndNotesBasedOnUnableToFillReason(fassMessage, order.UnableReason, order.AdjournedReason);
            
            if (FassOrderIsCancelled(fassMessage, order.CancelledReason)) SetFassCancellationCodeAndNotesBasedOnOrderCancelledReason(fassMessage, order.CancelledReason, order.AdjournedReason);

            if (FassOrderIsAdjourned(fassMessage, order.AdjournedReason)) SetFassAdjournedCodeAndNotesBasedOnOrderAdjournedReason(fassMessage, order.AdjournedReason);

            ConvertAndAssignFee(fassMessage, order);

            SendUpdateToMirth(fassMessage, trackedOrder, pcnOrder);
        }

        private static bool FassOrderIsUnableToFill(FassMonitorResponseMessage fassMessage, string orderUnableReason)
        {
            if (!string.Equals(fassMessage.OrderStatus, PcnIntegrationServicesConstants.OrderStatus.UnableToFill)) return false;

            return !string.IsNullOrWhiteSpace(orderUnableReason);
        }

        private static void SetFassUnableToFillCodeAndNotesBasedOnUnableToFillReason(FassMonitorResponseMessage fassMessage, string orderUnableReason, string orderAdjournedReason)
        {
            fassMessage.UnableToFillCode = orderUnableReason.Trim();
            fassMessage.Notes = orderAdjournedReason.Trim();
        }

        private static void SetFassClosingAttorney(FassMonitorResponseMessage fassMessage, OutboundOrder order)
        {
            fassMessage.AttorneyFirstName = order.ClosingAttorney.FirstName;
            fassMessage.AttorneyLastName = order.ClosingAttorney.LastName;
            fassMessage.AttorneyStreetAddress1 = order.ClosingAttorney.Address.Address1;
            fassMessage.AttorneyStreetAddress2 = order.ClosingAttorney.Address.Address2;
            fassMessage.AttorneyStreetAddress3 = order.ClosingAttorney.Address.Address3;
            fassMessage.AttorneyCity = order.ClosingAttorney.Address.City;
            fassMessage.AttorneyState = order.ClosingAttorney.Address.State;
            fassMessage.AttorneyZipCode = order.ClosingAttorney.Address.ZipCode;
            fassMessage.HomeNumber = order.ClosingAttorney.HomePhone;
            fassMessage.CellNumber = order.ClosingAttorney.CellPhone;
            fassMessage.WorkNumber = order.ClosingAttorney.WorkPhone;
            fassMessage.Fax = order.ClosingAttorney.FaxNumber1;
            fassMessage.Email = order.ClosingAttorney.Email1;
        }

        private static bool FassOrderIsAdjourned(FassMonitorResponseMessage fassMessage, string orderAdjournedReason)
        {
            if (!string.Equals(fassMessage.OrderStatus, PcnIntegrationServicesConstants.OrderStatus.Adjourned)) return false;

            return !string.IsNullOrWhiteSpace(orderAdjournedReason) && orderAdjournedReason.Contains("||");
        }

        private static void SetFassAdjournedCodeAndNotesBasedOnOrderAdjournedReason(FassMonitorResponseMessage fassMessage, string orderAdjournedReason)
        {
            var adjournedContent = orderAdjournedReason.Split(new [] {"||"}, StringSplitOptions.None);

            if (adjournedContent.Length < 2) return;

            fassMessage.AdjournedCode = adjournedContent[0].Trim();
            fassMessage.Notes = adjournedContent[1].Trim();
        }


        private static bool FassOrderIsCancelled(FassMonitorResponseMessage fassMessage, string orderCancelledReason)
        {
            if (!string.Equals(fassMessage.OrderStatus, PcnIntegrationServicesConstants.OrderStatus.Cancelled)) return false;

            return !string.IsNullOrWhiteSpace(fassMessage.OrderStatus) && !string.IsNullOrWhiteSpace(orderCancelledReason);
        }
        private static void SetFassCancellationCodeAndNotesBasedOnOrderCancelledReason(FassMonitorResponseMessage fassMessage, string orderCancelledReason, string orderAdjournedReason)
        {
            switch (orderCancelledReason.Trim())
            {
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AssociateChange:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AssociateChangeLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelled:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledDueToAFamilyEmergency:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledDueToAFamilyEmergencyLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledDueToCourt:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledDueToCourtLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledDueToASchedulingConflict:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledDueToASchedulingConflictLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledUnapproved:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledUnapprovedLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledDueToInclementWeather:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledDueToInclementWeatherLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyCancelledFaxbacks:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryBackedOutofSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyCancelledFaxbacksLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.BorrowerCancelled:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.BorrowerCancelledSigningNotaryDidNotMakeATrip;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.BorrowerCancelledLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.ClientCancelled:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.FassCancelledSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.ClientCancelledLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.ClientError:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.FassCancelledSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.ClientErrorLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.ClientRescheduledForANewDateTime:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.AppointmentRescheduled;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.ClientRescheduledForANewDateTimeLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.ClientRescheduledForANewClosingLocation:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.AppointmentRescheduled;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.ClientRescheduledForANewClosingLocationLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.DocumentsNotReadyInTime:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.LoanDocumentsNotAvailable;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.DocumentsNotReadyInTimeLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.LenderCancelled:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.EscrowLenderCancelledSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.LenderCancelledLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.NoClientResponseDocuments:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.LoanDocumentsNotAvailable;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.NoClientResponseDocumentsLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.NotaryDidNotCompleteConferenceCall:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryDidNotShowToSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.NotaryDidNotCompleteConferenceCallLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.AttorneyNoShow:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryDidNotShowToSigningAppointment;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.AttorneyNoShowLongDescription} {orderAdjournedReason}";
                    break;
                case PcnIntegrationServicesConstants.PcnCancellationReasons.ModCancellationAttorneyCouldNotScheduleWithBorrower:
                    fassMessage.CancellationCode = PcnIntegrationServicesConstants.FassCancellationReasons.NotaryUnableToScheduleLoanModSigning;
                    fassMessage.Notes = $"{PcnIntegrationServicesConstants.PcnCancellationReasonsLongDescriptions.ModCancellationAttorneyCouldNotScheduleWithBorrowerLongDescription} {orderAdjournedReason}";
                    break;
            }
        }
        private static bool IsNotMostRecentOrder(GetOrderResponse response)
        {
            if (response.GetOrderResult.Order == null) return true;

            return response.GetOrderResult.Order1 != null;
        }

        private static void SendUpdateToMirth(FassMonitorResponseMessage fassMessage, FassOrder trackedOrder, OSGPCN300 pcnOrder)
        {
            var mirthSender = new MirthService();
            var result = mirthSender.SendFassMessageToMirth(fassMessage.ToSerializedXml());
            if (result)
            {
                UpdateRecordOfActionTaken(trackedOrder, pcnOrder);
            }
        }

        private static void ConvertAndAssignFee(FassMonitorResponseMessage fassMessage, OutboundOrder order)
        {
            var nfi = CultureInfo.CurrentCulture.NumberFormat;
            nfi = (NumberFormatInfo) nfi.Clone();
            nfi.CurrencySymbol = string.Empty;
            fassMessage.Fee = string.Format(nfi, "{0:c}", order.TotalBillRate);
        }

        private static string GetNote(OutboundOrder order)
        {
            string pcnNote;
            switch (order.Status)
            {
                case PcnIntegrationServicesConstants.OrderStatus.Closed:
                    pcnNote = GetOrderTrackingNumbers(order.Couriers);
                    break;
                default:
                    pcnNote = string.Empty;
                    break;
            }
            return pcnNote;
        }

      private static string GetOrderTrackingNumbers(IReadOnlyCollection<Courier> couriers)
      {
        if (couriers.Count == 0) return string.Empty;

        var courierTrackingNumbers = new StringBuilder();

        foreach (var courier in couriers)
        {
          courierTrackingNumbers.Append(string.Format("Name: {0}, Tracking #: {1} ", courier.Name, courier.TrackingNumber));
        }

        return courierTrackingNumbers.ToString();
      }

        private static List<OSGPCN300> GetRecentOrdersFromPcn()
        {
            var result = new List<OSGPCN300>();
            try
            {
                var context = new PCNEntities();
                var currentDate = DateTime.Now.Date.AddDays(-Properties.Settings.Default.daysToLookBack);

                var res = context.OSGPCN300.Where(o =>
                (o.STATUS == 0 || o.STATUS == 1) && 
                (o.CUSTOMERID == Properties.Settings.Default.famsCustomerId || o.CUSTOMERID == Properties.Settings.Default.famsModificationsCustomerId) && 
                o.CREATE_DATE > currentDate &&
                o.PRODUCT == 9 || o.PRODUCT == 11
                );
                return res.ToList();
                
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
                return result;
            }
        }

        private static void AddOrderToMonitorDb(FassOrder order)
        {
            try
            {
                var context = new PCNIntegrationEntities();

                if (context.FassOrders.Any(o => o.OrderId == order.OrderId)) return;
                context.FassOrders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
            }
        }

        private static string ConvertStatusIdToString(int statusId)
        {
            switch (statusId)
            {
                case 0: return PcnIntegrationServicesConstants.OrderStatus.Pending;
                case 1: return PcnIntegrationServicesConstants.OrderStatus.Scheduled;
                case 2: return PcnIntegrationServicesConstants.OrderStatus.Cancelled;
                case 3: return PcnIntegrationServicesConstants.OrderStatus.UnableToFill;
                case 4: return PcnIntegrationServicesConstants.OrderStatus.Closed;
                case 5: return PcnIntegrationServicesConstants.OrderStatus.Adjourned;
                default: return string.Empty;
            }
        }

        private static List<FassOrder> GetTrackedOrders()
        {
            var result = new List<FassOrder>();

            try
            {
                var context= new PCNIntegrationEntities();
                result = context.FassOrders.ToList();
                return result;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
                return result;
            }
        }

        private static List<OSGPCN300> GetOrdersFromPcnById(List<string> orderIds)
        {
            var result = new List<OSGPCN300>();
            try
            {
                var context = new PCNEntities();
                var res = context.OSGPCN300.Where(o => orderIds.Contains(o.ORDERID)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
                return result;
            }
        }

        private static void UpdateRecordOfActionTaken(FassOrder fassOrder, OSGPCN300 order)
        {
            var context = new PCNIntegrationEntities();
            var fOrder = context.FassOrders.FirstOrDefault(f => f.OrderId == fassOrder.OrderId);

            if (fOrder == null) return;

            fOrder.StatusLastUpdated = fassOrder.Status;
            fOrder.Status = ConvertStatusIdToString(order.STATUS);
            fOrder.DateTimeLastUpdated = DateTime.Now;
        
            context.SaveChanges();
        }

        public override void BeginIntegrationProcessing()
        {
            TrackNewFassOrders();
            SendFassNotificationOnUpdate();
        }
    }
}