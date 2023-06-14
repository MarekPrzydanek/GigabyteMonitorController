using GigabyteMonitorController.Core.Exceptions;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;

namespace GigabyteMonitorController.Core.Models.P27QP;

internal class P27QP_Controller : IMonitorController
{
    public MonitorModel Model => Monitors.P27QP;

    public bool ToggleKvm()
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

        var currentState = ReadCurrentKvmState(device);
        var toggleKvmTo = currentState == KvmState.USB_B ? KvmState.USB_C : KvmState.USB_B;

        var buffer = CreateToggleKvmBuffer(toggleKvmTo);
        var packet = CreateWritePacket(buffer.Length);

        var sent = device.ControlTransfer(packet, buffer, 0, buffer.Length);

        return sent == buffer.Length;
    }

    private KvmState ReadCurrentKvmState(IUsbDevice device)
    {
        var requestBuffer = CreateReadKvmBuffer();
        var requestPacket = CreateWritePacket(requestBuffer.Length);

        var requestSent = device.ControlTransfer(requestPacket, requestBuffer, 0, requestBuffer.Length);
        if (requestSent != requestBuffer.Length)
        {
            throw new ReadRequestException(this);
        }

        var responseBuffer = new byte[12];
        var responsePacket = CreateReadPacket(responseBuffer.Length);

        var responseSent = device.ControlTransfer(responsePacket, responseBuffer, 0, responseBuffer.Length);
        if (responseSent != responseBuffer.Length)
        {
            throw new ReadResponseException(this);
        }

        var kvmState = responseBuffer[10];
        return kvmState switch
        {
            0 => KvmState.USB_B,
            1 => KvmState.USB_C,
            _ => throw new ControllerException(this, $"Invalid KVM state: ${kvmState}.")
        };
    }

    private UsbSetupPacket CreateReadPacket(int bufferLength) => new UsbSetupPacket
    {
        RequestType = 0xC0,
        Request = 162,
        Value = 0,
        Index = 111,
        Length = Convert.ToInt16(bufferLength)
    };

    private byte[] CreateReadKvmBuffer()
    {
        var data = new byte[] { 224, 105 };
        var buffer = new byte[] { 0x6E, 0x51, Convert.ToByte(0x81 + data.Length), 0x01 };
        return buffer.Concat(data).ToArray();
    }

    private UsbSetupPacket CreateWritePacket(int bufferLength) => new UsbSetupPacket
    {
        RequestType = 0x40,
        Request = 178,
        Value = 0,
        Index = 0,
        Length = Convert.ToInt16(bufferLength)
    };

    private byte[] CreateToggleKvmBuffer(KvmState toggleKvmTo)
    {
        var data = new byte[] { 224, 105, (byte)toggleKvmTo};
        var buffer = new byte[] { 0x6E, 0x51, Convert.ToByte(0x81 + data.Length), 0x03 };
        return buffer.Concat(data).ToArray();
    }
}
