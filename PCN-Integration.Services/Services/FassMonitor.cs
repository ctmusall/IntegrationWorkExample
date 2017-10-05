using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PCN_Integration.DataModels;
using PCN_Integration.Services.PcnIntegrationServiceTest;

namespace PCN_Integration.Services.Services
{
    public class FassMonitor : IntegrationServiceBase
    {
        private readonly int _daysToLookBack;
        private readonly int _mirthChannel;
        private readonly string _mirthIpAddress;
        public FassMonitor()
        {
            _mirthIpAddress = Properties.Settings.Default.mirthIPAddress;
            _mirthChannel = Properties.Settings.Default.mirthChannel;
            _daysToLookBack = Properties.Settings.Default.daysToLookBack;
        }

        private void TrackNewFassOrders()
        {
            var orders = GetRecentOrdersFromPCN();
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
            //1. Get all the TrackedFassOrders with pending status. Get thier Id's into a list.
            //2. Get those ids from PCN into a list.
            //3. Compare the orders. If there is a change in STATUS, send the message. 
      
            var TrackedFassOrders = GetTrackedOrders();
            var TrackedFassOrderIds = new List<string>();
            foreach (var fassOrder in TrackedFassOrders)
            {
                TrackedFassOrderIds.Add(fassOrder.OrderId);
            }

            var PcnOrders = GetOrdersFromPcnById(TrackedFassOrderIds);

            foreach (var trackedOrder in TrackedFassOrders)
            {
                //Check pcn for the Ids we have
                var pcnOrder = PcnOrders.Where(o => o.ORDERID.ToUpper() == trackedOrder.OrderId.ToUpper()).FirstOrDefault();
                //OrderId, Status, DateTimeLastUpdated, StatusLastUpdated, DateTimeCreated, DateTimeCompleted

                //IF STATUS CHANGES.

                if (trackedOrder.Status != ConvertStatusIdToString(pcnOrder.STATUS))
                {
                    var pcnWebService = new PcnWebServiceInvoker();
                    bool success;

                    //need to know the customer id on the observed order.... so need to query some id's on the first query... F55144 F55137
          
                    //var response = pcnWebService.GetOrderFromService("F55137", trackedOrder.OrderId.ToUpper().ToString(), out success); //also F55144 //todo handle this one also.                    

                    var response = pcnWebService.GetOrderFromService("F55137", pcnOrder.ORDERID, out success); //Being used for testing //also F55144 //todo handle this one also.                    

                    var order = response.GetOrderResult.Order;
                    var status = order.Status; // Pending, Scheduled, Cancelled, Unable to Fill, Closed, Adjourned
                    var note = GetNote(order);

                    var fassMessage = new FassMonitorResponseMessage()
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

                    //Convert and assign the fee
                    string fee;
                    NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
                    nfi = (NumberFormatInfo)nfi.Clone();
                    nfi.CurrencySymbol = "";
                    fee = string.Format(nfi, "{0:c}", order.TotalBillRate);
                    fassMessage.Fee = fee;

                    //Send Update
                    var mirthSender = new MirthService()
                    {
                        Ip = _mirthIpAddress,
                        Port = _mirthChannel,
                    };

                    var result = mirthSender.SendFassMessageToMirth(fassMessage.ToSerializedXml());
                    if (result.Success)
                    {
                        UpdateRecordOfActionTaken(trackedOrder, pcnOrder);
                    }
                    else
                    {
                        //error handling.
                    }
                }
            }
        }

        private string GetNote(OutboundOrder order)
        {
            string pcnNote;
            switch (order.Status)
            {
                case "Unable to Fill" :
                    pcnNote = order.UnableReason;
                    break;
                case "Cancelled":
                    pcnNote = order.CancelledReason;          
                    break;
                case "Adjourned":
                    pcnNote = order.AdjournedReason;
                    break;
                default:
                    pcnNote = "";
                    break;
            }

            //Now, we need to get that note from local db.
            //Add notes to local db.
            //create a call to get them.

            return "";
        }

        private List<OSGPCN300> GetRecentOrdersFromPCN()
        {
            var result = new List<OSGPCN300>();
            try
            {
                using (var context = new PCNEntities())
                {
                    DateTime currentDate = DateTime.Now.Date.AddDays(-_daysToLookBack);

                    var res = context.OSGPCN300.Where(o =>
                    (o.STATUS == 0 || o.STATUS == 1) && //o.ORDERID == "03072017" && //For testing
                    (o.CUSTOMERID == "F55137" || o.CUSTOMERID == "F55144") && //FAMS (F55137) and FAMS (F55144) loss mit
                    o.CREATE_DATE > currentDate
                    );
                    return res.ToList();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                //TODO Add error handling and logging.
                return result;
            }
        }

        private void AddOrderToMonitorDb(FassOrder order)
        {
            try
            {
                using (var context = new PCNIntegrationEntities())
                {
                    if (context.FassOrders.Any(o => o.OrderId == order.OrderId) == false)
                    {
                        context.FassOrders.Add(order);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO Add error handling and logging.
                var message = ex.Message;
            }
        }

        private string ConvertStatusIdToString(int statusId)
        {
            switch (statusId)
            {
                case 0: return "Pending";
                case 1: return "Scheduled";
                case 2: return "Cancelled";
                case 3: return "Unable to Fill";
                case 4: return "Closed";
                case 5: return "Adjourned";
                default: return "";
            }
        }

        private List<FassOrder> GetTrackedOrders()
        {
            var result = new List<FassOrder>();

            try
            {
                using (var context = new PCNIntegrationEntities())
                {
                    result = context.FassOrders.ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TODO Add error handling and logging.
                return result;
            }
        }

        private List<OSGPCN300> GetOrdersFromPcnById(List<string> OrderIds)
        {
            var result = new List<OSGPCN300>();
            try
            {
                using (var context = new PCNEntities())
                {
                    var res = context.OSGPCN300.Where(o => OrderIds.Contains(o.ORDERID)).ToList();
                    return res;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                //TODO Add error handling and logging.
                return result;
            }
        }

        private void UpdateRecordOfActionTaken(FassOrder fassOrder, OSGPCN300 order)
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