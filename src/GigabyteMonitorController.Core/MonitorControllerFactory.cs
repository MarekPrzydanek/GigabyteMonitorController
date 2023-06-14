using GigabyteMonitorController.Core.Models.M34WQ;
using GigabyteMonitorController.Core.Models.P27QP;

namespace GigabyteMonitorController.Core;

public class MonitorControllerFactory : IMonitorControllerFactory
{
    public IMonitorController P27QP() => new P27QP_Controller();
    public IMonitorController M34WQ() => new M34WQ_Controller();
}
