using GigabyteMonitorController.Core.Exceptions;
using HidLibrary;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;

namespace GigabyteMonitorController.Core.Models.M34WQ;

internal class M34WQ_Controller : IMonitorController
{
    public MonitorModel Model => Monitors.M34WQ;
    private ControllerOptions _options { get; }

    public M34WQ_Controller(ControllerOptions options) => _options = options;

    public async Task<bool> ToggleKvmAsync()
    {
        //using var context = new UsbContext();

        //var device = context.Find(x => x.VendorId == Model.VendorId && x.ProductId == Model.ProductId);

        //if (device == null)
        //{
        //    throw new DeviceNotFoundException(this);
        //}

        //if (!device.TryOpen())
        //{
        //    throw new FailedToOpenDeviceException(this);
        //}

        //var buffer = CreateBuffer();
        ////var packet = CreatePacket(Convert.ToInt16(buffer.Length));

        ////var sent = device.ControlTransfer(packet, buffer, 0, buffer.Length);

        //var writer = device.OpenEndpointWriter(WriteEndpointID.Ep01);
        //var error = device.(buffer, 0, buffer.Length, 1000, out var sent);

        //return Task.FromResult(sent == buffer.Length);

        using var device = HidDevices.Enumerate(Model.VendorId, Model.ProductId).FirstOrDefault();

        if (device == null)
        {
            throw new DeviceNotFoundException(this);
        }

        device.OpenDevice();

        if (!device.IsOpen)
        {
            throw new FailedToOpenDeviceException(this);
        }

        var buffer = CreateBuffer();

        var report = new HidReport(buffer.Length, new(buffer, HidDeviceData.ReadStatus.Success));

        var result = await device.WriteReportAsync(report);

        return result;
    }

    private UsbSetupPacket CreatePacket(short bufferLength) => new UsbSetupPacket
    {
        RequestType = 0x40,
        Request = 0x9,
        Value = 0x200,
        Index = 0,
        Length = bufferLength
    };

    private byte[] CreateBuffer()
    {
        var buffer = new byte[]
        {
            0x40, 0xC6, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x6E, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x51, 0x85, 0x03, 0xE0, 0x69, 0x00, 0x01, 0x5F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
        return buffer;
    }
}
