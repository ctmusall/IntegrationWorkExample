using System;
using System.IO;
using System.Xml.Serialization;

namespace PCN_Integration.Services
{
  public static class FormatFassMonitorResponsMessage 
  {
    public static string Format(FassMonitorResponseMessage message) 
    {
      var orderId = message.OrderId ?? "";
      var orderStatus = message.OrderStatus ?? "";
      var attorneyFirstName = message.AttorneyFirstName ?? "";
      var attorneyLastName = message.AttorneyLastName ?? "";
      var homeNumber = message.HomeNumber ?? "";
      var cellNumber = message.CellNumber ?? "";
      var workNumber = message.WorkNumber ?? "";
      var fax = message.Fax ?? "";
      var email = message.Email ?? "";
      var notes = message.Notes ?? "";
      var fee = message.Fee ?? "";

      string result = "";
      result = 
        "<message>" +
        "<OrderId>" + orderId + "</OrderId>" +
        "<OrderStatus>" + orderStatus + "</OrderStatus>" +
        "<AttorneyFirstName>" + attorneyFirstName + "</AttorneyFirstName>" +
        "<AttorneyLastName>" + attorneyLastName + "</AttorneyLastName>" +
        "<HomeNumber>" + homeNumber + "</HomeNumber>" +
        "<CellNumber>" + cellNumber + "</CellNumber>" +
        "<WorkNumber>" + workNumber + "</WorkNumber>" + 
        "<Fax>" + fax + "</Fax>" + 
        "<Email>" + email + "</Email>" + 
        "<Notes>" + notes + "</Notes>" + 
        "<Fee>" + fee + "</Fee>" +         
        "</message>";
      return result;
    }
  }

  public class FassMonitorResponseMessage
  {    
    public string OrderId { get; set; }
    public string OrderStatus { get; set; }
    public string AttorneyFirstName { get; set; }
    public string AttorneyLastName { get; set; }
    public string HomeNumber { get; set; }
    public string CellNumber { get; set; }
    public string WorkNumber { get; set; }
    public string Fax { get; set; }
    public string Email { get; set; }
    //public string ServiceIDs { get; set; }
    public string Notes { get; set; }
    public string Fee { get; set; }
    //public string SigningType { get; set; }     
    public string ToSerializedXml()
    {
      var serializer = new XmlSerializer(typeof(FassMonitorResponseMessage));
      using (var stringWriter = new StringWriter())
      {
        serializer.Serialize(stringWriter, this);
        return stringWriter.ToString();
      }
    }

  }
}
