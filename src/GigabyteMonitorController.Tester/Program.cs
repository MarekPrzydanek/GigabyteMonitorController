using GigabyteMonitorController.Core;

var factory = new MonitorControllerFactory(() => new()
{
    ToggleKvmTo = KvmState.USB_C
});

var p27qp = factory.P27QP();
var m34wq = factory.M34WQ();

try
{
    await Toggle(m34wq);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

async Task Toggle(IMonitorController controller)
{
    var result = await controller.ToggleKvmAsync();

    Console.Write($"[{controller.Model.Name}] Toggle KVM: {(result ? "success" : "fail")}");
}
