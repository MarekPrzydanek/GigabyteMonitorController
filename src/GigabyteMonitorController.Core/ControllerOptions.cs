namespace GigabyteMonitorController.Core;

public record ControllerOptions
{
    public KvmState ToggleKvmTo { get; init; }
}
