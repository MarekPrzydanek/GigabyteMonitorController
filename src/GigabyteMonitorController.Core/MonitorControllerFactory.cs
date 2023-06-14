using GigabyteMonitorController.Core.Models.M34WQ;
using GigabyteMonitorController.Core.Models.P27QP;

namespace GigabyteMonitorController.Core;

public class MonitorControllerFactory : IMonitorControllerFactory
{
    public ControllerOptions Options { get; }

    public MonitorControllerFactory(ControllerOptions options)
    {
        Options = options;
    }

    public MonitorControllerFactory(Func<ControllerOptions> configureOptions) : this(configureOptions()) 
    {
    }

    public IMonitorController P27QP() => new P27QP_Controller(Options);
    public IMonitorController M34WQ() => new M34WQ_Controller(Options);
}
