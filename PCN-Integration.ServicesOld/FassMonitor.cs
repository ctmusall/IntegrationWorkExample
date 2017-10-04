using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCN_Integration.DataModels;
using PCN_Integration.Services;
using System.Data;

namespace PCN_Integration.Services
{

  public class FassMonitor
  {
    public List<OSGPCN300> GetRecentOrdersFromPCN()
    {
      var result = new List<OSGPCN300>();
      try
      {
        using (var context = new PCNEntities())
        {
          DateTime currenteDate = DateTime.Now.Date.AddDays(-2);

          var res = context.OSGPCN300.Where(o =>
          //GET PENDING ORDERS
          o.STATUS == 0 &&

          //FROM FAMS
          o.CUSTOMERID == "T22900" &&

          //FROM THE LAST 2 DAYS
          o.CREATE_DATE > currenteDate
          )
          .Take(200).ToList();

          return res;
          //return context.OSGPCN300.Where(o => o.ORDERDATE >= currenteDate && o.STATUS.ToString() == "Pending").ToList();
        }
      }
      catch (Exception ex)
      {
        var errorMessage = ex.Message;
        //todo Add error handling and logging.
        return result;
      }
    }

    public void AddOrderToMonitorDb(FassOrder order)
    {
      try
      {
        using (var context = new IntegrationModel())
        {
          if (context.FassOrders.Any(o => o.OrderId == order.OrderId) == false)
          {
            context.FassOrders.Add(order);
            context.SaveChanges();
          }
        }
      }
      catch (Exception)
      {
        //todo Add error handling and logging.
        //throw;
      }
    }

    public void ProcessFassNotification()
    {
      List<OSGPCN300> orders = GetRecentOrdersFromPCN();
      if (orders.Count > 0)
      {
        foreach (var order in orders)
        {
          AddOrderToMonitorDb(new FassOrder
          {
            OrderId = order.ORDERID,
            Status = order.STATUS.ToString(),
            DateTimeLastUpdated = order.CREATE_DATE,
            StatusLastUpdated = order.STATUS.ToString(),
            DateTimeCreated = order.CREATE_DATE.ToString(),
          });
        }
      }
    }

    public List<FassOrder> GetTrackedOrders()
    {
      var result = new List<FassOrder>();

      try
      {
        using (var context = new IntegrationModel())
        {
          result = context.FassOrders.Where(o => o.Status == "2").ToList();
          return result;
        }
      }
      catch (Exception)
      {
        //todo Add error handling and logging.
        return result;
        //throw;
      }
    }

    public List<OSGPCN300> GetOrdersFromPCN(List<string> OrderIds)
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
        //todo Add error handling and logging.
        return result;
      }
    }

    public void ProcessFams()
    {
      //Get all the TrackedFassOrders with pending status. Get thier Id's into a list.
      //Get those ids from PCN into a list.
      //Compare those statusues.... then send the message.

      var TrackedFassOrders = GetTrackedOrders();
      var TrackedFassOrderIds = new List<string>();
      foreach (var fassOrder in TrackedFassOrders)
      {
        TrackedFassOrderIds.Add(fassOrder.OrderId);
      }

      var PcnOrders = GetOrdersFromPCN(TrackedFassOrderIds);

      foreach (var fOrder in TrackedFassOrders)
      {
        var pcnOrder = PcnOrders.Where(o => o.ORDERID == fOrder.OrderId).Single(); //It better damn well be single...
        if (fOrder.Status != pcnOrder.STATUS.ToString())
        {

          var msg = GenerateFassResponseMessage("orderId", "orderStatus", "attorneyFirstName", "attorneyLastName",
          "homeNumber", "cellNumber", "workNumber", "fax", "email", new List<string>() {"serviceIds"},
          "notes", "fee", "signingType");

          msg.FormatedMessage();
          //Send to FASS and/or Mirth.
        }
      }
      //var pcnOrder = new OSGPCN300();
    }

    public FassMonitorResponseMessage GenerateFassResponseMessage(string orderId, string orderStatus, string attorneyFirstName, string attorneyLastName,
      string homeNumber, string cellNumber, string workNumber, string fax, string email, List<string> serviceIds,
      string notes, string fee, string signingType)
    {
      string serviceIdsStr = "";
      if (serviceIds.Count() > 0)
      {        
        foreach (var svc in serviceIds)
        {
          if (String.IsNullOrEmpty(serviceIdsStr))
          {
            serviceIdsStr = serviceIdsStr + svc;
          }
          else
          {
            serviceIdsStr = serviceIdsStr + "|" + svc;
          }         
        }
      }

      var msg = new FassMonitorResponseMessage()
      {
        OrderId = orderId ?? "",
        OrderStatus = orderStatus ?? "",
        AttorneyFirstName = attorneyFirstName ?? "",
        AttorneyLastName = attorneyLastName ?? "",
        HomeNumber = homeNumber ?? "",
        CellNumber = cellNumber ?? "",
        WorkNumber = workNumber ?? "",
        Fax = fax ?? "",
        Email = email ?? "",
        Notes = notes ?? "",
        ServiceIDs = serviceIdsStr ?? "",
        Fee = "",
        SigningType = ""
      };

      return msg;
    }
  }

}
