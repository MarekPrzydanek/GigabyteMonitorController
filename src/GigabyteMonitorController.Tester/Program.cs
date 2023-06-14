using GigabyteMonitorController.Core;

var factory = new MonitorControllerFactory();

var p27qp = factory.P27QP();
var m34wq = factory.M34WQ();

try
{
    Toggle(p27qp);
    Toggle(m34wq);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

void Toggle(IMonitorController controller)
{
    var result = controller.ToggleKvm();

    Console.WriteLine($"[{controller.Model.Name}] Toggle KVM: {(result ? "success" : "fail")}");
}
