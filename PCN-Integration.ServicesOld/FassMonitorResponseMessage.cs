using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCN_Integration.Services
{
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
    public string ServiceIDs { get; set; }
    public string Notes { get; set; }
    public string Fee { get; set; }
    public string SigningType { get; set; }

    

    private string template
      = @"&lt;message&gt;
              &lt;OrderId&gt;{1}&lt;/OrderId&gt;
              &lt;OrderStatus&gt;{2}&lt;/OrderStatus&gt;
              &lt;AttorneyFirstName&gt;{3}&lt;/AttorneyFirstName&gt;
              &lt;AttorneyLastName&gt;{4}&lt;/AttorneyLastName&gt;
              &lt;HomeNumber&gt;{5}&lt;/HomeNumber&gt;
              &lt;CellNumber&gt;{6}&lt;/CellNumber&gt;
              &lt;WorkNumber&gt;{7}&lt;/WorkNumber&gt;
              &lt;Fax&gt;{8}&lt;/Fax&gt;
              &lt;Email&gt;{9}&lt;/Email&gt;
              &lt;ServiceIDs&gt;{10}/ServiceIDs&gt;
              &lt;Notes&gt;{11}&lt;/Notes&gt;
              &lt;Fee&gt;{12}&lt;/Fee&gt;
              &lt;SigningType&gt;{13}&lt;/SigningType&gt;
        &lt;/message&gt;";

    public string FormatedMessage()
    {
      return string.Format(template,
        OrderId ?? "",
        OrderStatus ?? "",
        AttorneyFirstName ?? "",
        AttorneyLastName ?? "",
        HomeNumber ?? "",
        CellNumber ?? "",
        WorkNumber ?? "",
        Fax ?? "",
        Email ?? "",
        ServiceIDs ?? "",
        Notes ?? "",
        Fee ?? "",
        SigningType ?? "");
    }
  }
}
