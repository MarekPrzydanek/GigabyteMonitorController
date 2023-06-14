using GigabyteMonitorController.Core.Exceptions;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;

namespace GigabyteMonitorController.Core.Models.P27QP;

internal class P27QP_Controller : IMonitorController
{
    public MonitorModel Model => Monitors.P27QP;
    private ControllerOptions _options { get; }

    public P27QP_Controller(ControllerOptions options) => _options = options;

    public Task<bool> ToggleKvmAsync()
    {
        using var context = new UsbContext();

        var device = context.Find(x => x.VendorId == Model.VendorId && x.ProductId == Model.ProductId);
        
        if (device == null)
        {
            throw new DeviceNotFoundException(this);
        }

        if (!device.TryOpen())
        {
            throw new FailedToOpenDeviceException(this);
        }

        var buffer = CreateBuffer();
        var packet = CreatePacket(Convert.ToInt16(buffer.Length));

        var sent = device.ControlTransfer(packet, buffer, 0, buffer.Length);

        return Task.FromResult(sent == buffer.Length);
    }

    private UsbSetupPacket CreatePacket(short bufferLength) => new UsbSetupPacket
    {
        RequestType = 0x40,
        Request = 178,
        Value = 0,
        Index = 0,
        Length = bufferLength
    };

    private byte[] CreateBuffer()
    {
        var data = new byte[] { 224, 105, (byte)_options.ToggleKvmTo };
        var buffer = new byte[] { 0x6E, 0x51, Convert.ToByte(0x81 + data.Length), 0x03 };
        return buffer.Concat(data).ToArray();
    }
}
