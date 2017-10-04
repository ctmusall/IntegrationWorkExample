//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PCN_Integration.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class OSGPCN300
    {
        public string ORDERID { get; set; }
        public int INSTRUCTIONID { get; set; }
        public short STATUS { get; set; }
        public string CUSTOMERID { get; set; }
        public short NEEDSUPDATED { get; set; }
        public string NOTES { get; set; }
        public string SCHEDULINGNOTES { get; set; }
        public System.DateTime ORDERDATE { get; set; }
        public System.DateTime ORDERTIME { get; set; }
        public string LENDER_NAME { get; set; }
        public int CLOSING_LOCATION { get; set; }
        public string CLOSING_ADDRESS1 { get; set; }
        public string CLOSING_ADDRESS2 { get; set; }
        public string CLOSING_ADDRESS3 { get; set; }
        public string CLOSING_CITY { get; set; }
        public string CLOSING_STATE { get; set; }
        public string CLOSING_ZIP { get; set; }
        public short PRODUCT { get; set; }
        public System.DateTime CLOSINGDATE { get; set; }
        public short CLOSINGDATETBD { get; set; }
        public string BORROWERNAME { get; set; }
        public string COBORROWERNAME { get; set; }
        public string BORROWER_ADDRESS1 { get; set; }
        public string BORROWER_ADDRESS2 { get; set; }
        public string BORROWER_ADDRESS3 { get; set; }
        public string BORROWER_CITY { get; set; }
        public string BORROWER_STATE { get; set; }
        public string BORROWER_ZIP { get; set; }
        public string BORROWER_PHONE1 { get; set; }
        public string BORROWER_PHONE2 { get; set; }
        public string BORROWER_EMAIL { get; set; }
        public short DOCSTOATTORNEY { get; set; }
        public decimal TOTALBILLAMT { get; set; }
        public decimal TOTALPAYAMT { get; set; }
        public string CREATE_USER { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public string MODIFY_USER { get; set; }
        public System.DateTime MODIFY_DATE { get; set; }
        public int CUSTOMER_CONTACT { get; set; }
        public string TRACKING1 { get; set; }
        public string TRACKING2 { get; set; }
        public string TRACKING3 { get; set; }
        public System.DateTime CLOSINGTIME { get; set; }
        public short CLOSINGTIMETBD { get; set; }
        public int COUNTYID { get; set; }
        public int RESPONSETIME { get; set; }
        public System.DateTime SCHEDULEDTIME { get; set; }
        public System.DateTime CONFIRMATIONSENT { get; set; }
        public string INSTRUCTIONS { get; set; }
        public System.DateTime IMPORTDATE { get; set; }
        public short CANCELLEDREASON { get; set; }
        public short UNABLEREASON { get; set; }
        public string FAILEDREASON { get; set; }
        public string SCHEDULE_USER { get; set; }
        public System.DateTime SCHEDULE_DATE { get; set; }
        public int BORROWER_COUNTYID { get; set; }
        public int TRACKING_COURIER_ID_1 { get; set; }
        public int TRACKING_COURIER_ID_2 { get; set; }
        public int TRACKING_COURIER_ID_3 { get; set; }
        public System.DateTime CONFIRMDATE { get; set; }
        public System.DateTime CONFIRMTIME { get; set; }
        public System.DateTime REQUESTEDDATE { get; set; }
        public System.DateTime REQUESTEDTIME { get; set; }
        public string ADDITIONALSERVICE { get; set; }
        public System.DateTime DATE_RECORDED { get; set; }
        public string BOOK { get; set; }
        public string PAGE { get; set; }
        public System.DateTime DISBURSMENT_SENT_ATTY { get; set; }
        public bool MANAGER_APPROVAL { get; set; }
        public string FILE_NUM { get; set; }
        public Nullable<int> PRECLOSECALL { get; set; }
        public Nullable<System.DateTime> PRECLOSECALLTIME { get; set; }
        public Nullable<int> POSTCLOSEATTCALL { get; set; }
        public Nullable<System.DateTime> POSTCLOSEATTCALLTIME { get; set; }
        public Nullable<int> POSTCLOSECLNTCALL { get; set; }
        public Nullable<System.DateTime> POSTCLOSECLNTCALLTIME { get; set; }
        public int DIDNOTCLOSEREASON { get; set; }
        public Nullable<System.DateTime> PRECLOSE { get; set; }
        public Nullable<System.DateTime> POSTCLOSEATT { get; set; }
        public Nullable<System.DateTime> POSTCLOSECLNT { get; set; }
        public int IVRCALLSTATUS { get; set; }
        public string RESCHEDULEDTIME { get; set; }
        public int OSG_ROWID { get; set; }
        public Nullable<int> SOP_SEQUENCE { get; set; }
        public Nullable<int> PM_SEQUENCE { get; set; }
        public Nullable<System.DateTime> SOP_IMPORTDATE { get; set; }
        public Nullable<System.DateTime> PM_IMPORTDATE { get; set; }
    }
}
