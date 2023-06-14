namespace GigabyteMonitorController.Core.Exceptions;

public class ReadResponseException : ControllerException
{
    public ReadResponseException(IMonitorController controller) : base(controller, "Error while receiving read response.")
    {
    }
}
