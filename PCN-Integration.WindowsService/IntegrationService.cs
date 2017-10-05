using System.ServiceProcess;
using PCN_Integration.Services;

namespace PCN_Integration.WindowsService
{
  public partial class IntegrationService : ServiceBase
  {
    public IntegrationService()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      EventLog.WriteEntry("PCN-Integration Windows Service has started.");
    }    

    protected override void OnStop()
    {
      EventLog.WriteEntry("PCN-Integration Windows Service has stopped.");
    }

    private void CreateAndStartFassIntegrationService()
    {
      var fass = new FassMonitor();
      fass.TrackNewFassOrders();
      fass.SendFassNotificationOnUpdate();
    }
  }
}
