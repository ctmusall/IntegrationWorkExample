﻿using System.IO;
using System.Xml.Serialization;

namespace PCN_Integration.Services.Models
{
    public class FassMonitorResponseMessage
    {    
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string AttorneyFirstName { get; set; }
        public string AttorneyLastName { get; set; }
        public string AttorneyStreetAddress1 { get; set; }
        public string AttorneyStreetAddress2 { get; set; }
        public string AttorneyStreetAddress3 { get; set; }
        public string AttorneyCity { get; set; }
        public string AttorneyState { get; set; }
        public string AttorneyZipCode { get; set; }
        public string HomeNumber { get; set; }
        public string CellNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string Fee { get; set; }
        public string CancellationCode { get; set; }
        public string AdjournedCode { get; set; }
        public string UnableToFillCode { get; set; }
        internal string ToSerializedXml()
        {
            var serializer = new XmlSerializer(typeof(FassMonitorResponseMessage));
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }
    }
}
