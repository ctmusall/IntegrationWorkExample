using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCN_Integration.DataModels;
using System.Data;
using PCN_Integration.Services;

namespace PCN_Integration.Console 
{
  class Program
  {
    static void Main(string[] args)
    {
      //TODO - Move to Windows Service
      var fass = new FassMonitor();
      fass.TrackNewFassOrders();
      fass.SendFassNotificationOnUpdate();
    }
  }
}