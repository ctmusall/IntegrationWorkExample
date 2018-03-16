using ReswareOrderMonitorService.ReswareSigning;

namespace ReswareOrderMonitorService.Repositories
{
    internal interface IReceiveSigningServiceRepository
    {
        SigningServiceResult[] GetAllSignings();
    }
}
