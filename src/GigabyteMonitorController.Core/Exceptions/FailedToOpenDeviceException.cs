namespace GigabyteMonitorController.Core.Exceptions;

internal class FailedToOpenDeviceException : ControllerException
{
    public FailedToOpenDeviceException(IMonitorController controller) : base(controller, "Failed to open the device.")
    {
    }
}
