using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace PCN_Integration.Services
{
  public class OrderMessage
  {
    //camelCase for mirth integration TODO Change to standard case and fix in mirth
    public string orderId { get; set; }
    public string customerId { get; set; }
    public string customerContact { get; set; }
    public string customerCompany { get; set; }
    public string lenderName { get; set; }
    public string borrowerFirstName { get; set; }
    public string borrowerMiddleName { get; set; }
    public string borrowerLastName { get; set; }
    public string borrowerSuffix { get; set; }
    public string coborrowerFirstName { get; set; }
    public string coborrowerMiddleName { get; set; }
    public string coborrowerLastName { get; set; }
    public string coborrowerSuffix { get; set; }
    public string product { get; set; }
    public string fileNumber { get; set; }
    public string orderRequestedDate { get; set; }
    public string orderRequestedTime { get; set; }
    public string closingDate { get; set; }
    public string closingTime { get; set; }
    public string closingLocation { get; set; }
    public string borrowerAddress1 { get; set; }
    public string borrowerAddress2 { get; set; }
    public string borrowerAddress3 { get; set; }
    public string borrowerCity { get; set; }
    public string borrowerState { get; set; }
    public string borrowerZipCode { get; set; }
    public string borrowerCounty { get; set; }
    public string borrowerPhone1 { get; set; }
    public string borrowerPhone2 { get; set; }
    public string borrowerEmail1 { get; set; }
    public string closingAddress1 { get; set; }
    public string closingAddress2 { get; set; }
    public string closingAddress3 { get; set; }
    public string closingCity { get; set; }
    public string closingState { get; set; }
    public string closingZipCode { get; set; }
    public string closingCounty { get; set; }
    public string notes { get; set; }
    public string service1 { get; set; }
    public string service2 { get; set; }
    public string service3 { get; set; }
    public string service4 { get; set; }
    public string service5 { get; set; }
    public string service6 { get; set; }
    public string service7 { get; set; }
    public string service8 { get; set; }
    public string service9 { get; set; }
    public string service10 { get; set; }
    public string docsToAttorney { get; set; }
    public string orderAction { get; set; }

    //TODO This works, but mirth doesn't recognize it.
    //[XmlArray("Services")]
    //[XmlArrayItem("Service")]
    //public List<string> service { get; set; }

    public OrderMessage()
    {

    }

    public OrderMessage(OrderMessage order)
    {
      orderId = order.orderId;
      customerId = order.customerId;
      customerContact = order.customerContact;
      customerCompany = order.customerCompany;
      lenderName = order.lenderName;
      borrowerFirstName = order.borrowerFirstName;
      borrowerLastName = order.borrowerLastName;
      coborrowerFirstName = order.coborrowerFirstName;
      coborrowerLastName = order.coborrowerLastName;
      product = order.product;
      fileNumber = order.fileNumber;
      orderRequestedDate = order.orderRequestedDate;
      orderRequestedTime = order.orderRequestedTime;
      closingDate = order.closingDate;
      closingTime = order.closingTime;
      closingLocation = order.closingLocation;
      borrowerAddress1 = order.borrowerAddress1;
      borrowerAddress2 = order.borrowerAddress2;
      borrowerAddress3 = order.borrowerAddress3;
      borrowerCity = order.borrowerCity;
      borrowerState = order.borrowerState;
      borrowerZipCode = order.borrowerZipCode;
      borrowerCounty = order.borrowerCounty;
      borrowerPhone1 = order.borrowerPhone1;
      borrowerPhone2 = order.borrowerPhone2;
      borrowerEmail1 = order.borrowerEmail1;
      closingAddress1 = order.closingAddress1;
      closingAddress2 = order.closingAddress2;
      closingAddress3 = order.closingAddress3;
      closingCity = order.closingCity;
      closingState = order.closingState;
      closingZipCode = order.closingZipCode;
      closingCounty = order.closingCounty;
      notes = order.notes;
      service1 = order.service1;
      service2 = order.service2;
      service3 = order.service3;
      service4 = order.service4;
      service5 = order.service5;
      service6 = order.service6;
      service7 = order.service7;
      service8 = order.service8;
      service9 = order.service9;
      service10 = order.service10;
      docsToAttorney = order.docsToAttorney;
      orderAction = order.orderAction;
    }

    public string Serialize()
    {
      var serializer = new XmlSerializer(typeof(OrderMessage));
      using (var stringWriter = new StringWriter())
      {
        serializer.Serialize(stringWriter, this);
        return stringWriter.ToString();
      }
    }

    /// <summary>
    /// Checks each property of the input order message and if it is not null or empty, replaces the property value
    /// </summary>
    /// <param name="existingMessage"></param>
    public void MapFamsNc(OrderMessage existingMessage)
    {
      //if (!String.IsNullOrEmpty(existingMessage.orderId) && !String.IsNullOrEmpty(orderId)) orderId = existingMessage.orderId;
      //if (!String.IsNullOrEmpty(existingMessage.customerId)) customerId = existingMessage.customerId;
      //if (!String.IsNullOrEmpty(existingMessage.customerContact)) customerContact = existingMessage.customerContact;
      if (!String.IsNullOrEmpty(existingMessage.customerCompany)) customerCompany = existingMessage.customerCompany;
      if (!String.IsNullOrEmpty(existingMessage.lenderName)) lenderName = existingMessage.lenderName;
      if (!String.IsNullOrEmpty(existingMessage.borrowerFirstName)) borrowerFirstName = existingMessage.borrowerFirstName;
      if (!String.IsNullOrEmpty(existingMessage.borrowerLastName)) borrowerLastName = existingMessage.borrowerLastName;
      if (!String.IsNullOrEmpty(existingMessage.coborrowerFirstName)) coborrowerFirstName = existingMessage.coborrowerFirstName;
      if (!String.IsNullOrEmpty(existingMessage.coborrowerLastName)) coborrowerLastName = existingMessage.coborrowerLastName;
      //if (!String.IsNullOrEmpty(existingMessage.product)) product = existingMessage.product;
      //if (!String.IsNullOrEmpty(existingMessage.fileNumber)) fileNumber = existingMessage.fileNumber;
      //if (!String.IsNullOrEmpty(existingMessage.orderRequestedDate)) orderRequestedDate = existingMessage.orderRequestedDate;
      //if (!String.IsNullOrEmpty(existingMessage.orderRequestedTime)) orderRequestedTime = existingMessage.orderRequestedTime;
      //if (!String.IsNullOrEmpty(existingMessage.closingDate)) closingDate = existingMessage.closingDate;
      //if (!String.IsNullOrEmpty(existingMessage.closingTime)) closingTime = existingMessage.closingTime;
      if (!String.IsNullOrEmpty(existingMessage.closingLocation)) closingLocation = existingMessage.closingLocation;
      if (!String.IsNullOrEmpty(existingMessage.borrowerAddress1)) borrowerAddress1 = existingMessage.borrowerAddress1;
      if (!String.IsNullOrEmpty(existingMessage.borrowerAddress2)) borrowerAddress2 = existingMessage.borrowerAddress2;
      if (!String.IsNullOrEmpty(existingMessage.borrowerAddress3)) borrowerAddress3 = existingMessage.borrowerAddress3;
      if (!String.IsNullOrEmpty(existingMessage.borrowerCity)) borrowerCity = existingMessage.borrowerCity;
      if (!String.IsNullOrEmpty(existingMessage.borrowerState)) borrowerState = existingMessage.borrowerState;
      if (!String.IsNullOrEmpty(existingMessage.borrowerZipCode)) borrowerZipCode = existingMessage.borrowerZipCode;
      if (!String.IsNullOrEmpty(existingMessage.borrowerCounty)) borrowerCounty = existingMessage.borrowerCounty;
      if (!String.IsNullOrEmpty(existingMessage.borrowerPhone1)) borrowerPhone1 = existingMessage.borrowerPhone1;
      if (!String.IsNullOrEmpty(existingMessage.borrowerPhone2)) borrowerPhone2 = existingMessage.borrowerPhone2;
      if (!String.IsNullOrEmpty(existingMessage.closingAddress1)) closingAddress1 = existingMessage.closingAddress1;
      if (!String.IsNullOrEmpty(existingMessage.closingAddress2)) closingAddress2 = existingMessage.closingAddress2;
      if (!String.IsNullOrEmpty(existingMessage.closingAddress3)) closingAddress3 = existingMessage.closingAddress3;
      if (!String.IsNullOrEmpty(existingMessage.closingCity)) closingCity = existingMessage.closingCity;
      if (!String.IsNullOrEmpty(existingMessage.closingState)) closingState = existingMessage.closingState;
      if (!String.IsNullOrEmpty(existingMessage.closingZipCode)) closingZipCode = existingMessage.closingZipCode;
      if (!String.IsNullOrEmpty(existingMessage.closingCounty)) closingCounty = existingMessage.closingCounty;
      if (!String.IsNullOrEmpty(existingMessage.notes)) notes = existingMessage.notes;
      //if (!String.IsNullOrEmpty(existingMessage.service1)) service1 = existingMessage.service1;
      //if (!String.IsNullOrEmpty(existingMessage.service2)) service2 = existingMessage.service2;
      //if (!String.IsNullOrEmpty(existingMessage.service3)) service3 = existingMessage.service3;
      //if (!String.IsNullOrEmpty(existingMessage.service4)) service4 = existingMessage.service4;
      //if (!String.IsNullOrEmpty(existingMessage.service5)) service5 = existingMessage.service5;
      //if (!String.IsNullOrEmpty(existingMessage.service6)) service6 = existingMessage.service6;
      //if (!String.IsNullOrEmpty(existingMessage.service7)) service7 = existingMessage.service7;
      //if (!String.IsNullOrEmpty(existingMessage.service8)) service8 = existingMessage.service8;
      //if (!String.IsNullOrEmpty(existingMessage.service9)) service9 = existingMessage.service9;
      //if (!String.IsNullOrEmpty(existingMessage.service10)) service10 = existingMessage.service10;
    }

    /// <summary>
    /// Copies closing address information from a different order message into the borrower address
    /// </summary>
    /// <param name="existingMessage"></param>
    public void CopyClosingAddressToBorrowerAddress(OrderMessage existingMessage)
    {
      borrowerAddress1 = existingMessage.closingAddress1;
      borrowerAddress2 = existingMessage.closingAddress2;
      borrowerAddress3 = existingMessage.closingAddress3;

      borrowerCity = existingMessage.closingCity;
      borrowerState = existingMessage.borrowerState;
      borrowerCounty = existingMessage.closingCounty;
      borrowerZipCode = existingMessage.closingZipCode;
    }

    public void CopyBorrowerAddressToClosingAddress(OrderMessage existingMessage)
    {
      closingAddress1 = existingMessage.borrowerAddress1;
      closingAddress2 = existingMessage.borrowerAddress2;
      closingAddress3 = existingMessage.borrowerAddress3;

      closingCity = existingMessage.borrowerCity;
      closingState = existingMessage.borrowerState;
      closingCounty = existingMessage.borrowerCounty;
      closingZipCode = existingMessage.borrowerZipCode;
    }

  }
}
