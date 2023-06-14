namespace GigabyteMonitorController.Core.Exceptions;
public class ControllerException : Exception
{
    public ControllerException(IMonitorController controller, string message) : base($"Error in ${controller.Model}: ${message}")
    {
    }
}
