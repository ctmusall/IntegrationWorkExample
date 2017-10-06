using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using PCN_Integration.DataModels;
using PCN_Integration.Services.Common;
using PCN_Integration.Services.Models;
using PCN_Integration.Services.PcnIntegrationServiceTest;

namespace PCN_Integration.Services.Services
{
    public class FassMonitor : IntegrationServiceBase
    {
        private void TrackNewFassOrders()
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

        private void SendFassNotificationOnUpdate()
        {     
            var trackedFassOrders = GetTrackedOrders();
            var trackedFassOrderIds = trackedFassOrders.Select(fassOrder => fassOrder.OrderId).ToList();

            var pcnOrders = GetOrdersFromPcnById(trackedFassOrderIds);

            foreach (var trackedOrder in trackedFassOrders)
            {
                BuildFassResponseMessageAndSendUpdate(pcnOrders, trackedOrder);
            }
        }

        private void BuildFassResponseMessageAndSendUpdate(IEnumerable<OSGPCN300> pcnOrders, FassOrder trackedOrder)
        {
            var pcnOrder = pcnOrders.FirstOrDefault(o =>
                string.Equals(o.ORDERID, trackedOrder.OrderId, StringComparison.InvariantCultureIgnoreCase));

            if (pcnOrder == null) return;

            if (trackedOrder.Status == ConvertStatusIdToString(pcnOrder.STATUS)) return;

            var pcnWebService = new PcnWebServiceInvoker();
            bool success;

            var response = pcnWebService.GetOrderFromService("F55137", pcnOrder.ORDERID,out success); //Being used for testing //also F55144                

            var order = response.GetOrderResult.Order;
            var status = order.Status;
            var note = GetNote(order);

            var fassMessage = new FassMonitorResponseMessage
            {
                OrderId = order.OrderId,
                OrderStatus = status,
                AttorneyFirstName = order.ClosingAttorney.FirstName,
                AttorneyLastName = order.ClosingAttorney.LastName,
                HomeNumber = order.ClosingAttorney.HomePhone,
                CellNumber = order.ClosingAttorney.CellPhone,
                WorkNumber = order.ClosingAttorney.WorkPhone,
                Fax = order.ClosingAttorney.FaxNumber1,
                Email = order.ClosingAttorney.Email1,
                Notes = note,
            };

            ConvertAndAssignFee(fassMessage, order);

            SendUpdateToMirth(fassMessage, trackedOrder, pcnOrder);
        }

        private void SendUpdateToMirth(FassMonitorResponseMessage fassMessage, FassOrder trackedOrder, OSGPCN300 pcnOrder)
        {
            var mirthSender = new MirthService
            {
                Ip = Properties.Settings.Default.mirthIPAddress,
                Port = Properties.Settings.Default.mirthChannel
            };

            var result = mirthSender.SendFassMessageToMirth(fassMessage.ToSerializedXml());
            if (result.Success)
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
                case PcnIntegrationServicesConstants.OrderStatus.UnableToFill:
                    pcnNote = order.UnableReason;
                    break;
                case PcnIntegrationServicesConstants.OrderStatus.Cancelled:
                    pcnNote = order.CancelledReason;          
                    break;
                case PcnIntegrationServicesConstants.OrderStatus.Adjourned:
                    pcnNote = order.AdjournedReason;
                    break;
                default:
                    pcnNote = string.Empty;
                    break;
            }
            return pcnNote;
        }

        private List<OSGPCN300> GetRecentOrdersFromPcn()
        {
            var result = new List<OSGPCN300>();
            try
            {
                var context = new PCNEntities();
                var currentDate = DateTime.Now.Date.AddDays(-Properties.Settings.Default.daysToLookBack);

                var res = context.OSGPCN300.Where(o =>
                (o.STATUS == 0 || o.STATUS == 1) && //o.ORDERID == "03072017" && //For testing
                (o.CUSTOMERID == "F55137" || o.CUSTOMERID == "F55144") && //FAMS (F55137) and FAMS (F55144) loss mit
                o.CREATE_DATE > currentDate
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