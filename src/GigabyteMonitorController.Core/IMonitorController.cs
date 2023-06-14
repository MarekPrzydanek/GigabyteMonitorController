namespace GigabyteMonitorController.Core;

public interface IMonitorController
{
    MonitorModel Model { get; }

    Task<bool> ToggleKvmAsync();
}
