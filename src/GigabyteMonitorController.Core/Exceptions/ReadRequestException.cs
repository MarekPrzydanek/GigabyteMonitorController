namespace GigabyteMonitorController.Core.Exceptions;

public class ReadRequestException : ControllerException
{
    public ReadRequestException(IMonitorController controller) : base(controller, "Error while sending read request.")
    {
    }
}
