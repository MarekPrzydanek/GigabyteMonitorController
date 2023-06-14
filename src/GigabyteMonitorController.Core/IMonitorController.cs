namespace GigabyteMonitorController.Core;

public interface IMonitorController
{
    MonitorModel Model { get; }

    bool ToggleKvm();
}
