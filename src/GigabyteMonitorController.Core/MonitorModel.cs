namespace GigabyteMonitorController.Core;

public record MonitorModel
{
    public int VendorId { get; }
    public int ProductId { get; }
    public string Name { get; }

    public MonitorModel(int vendorId, int productId, string name)
    {
        VendorId = vendorId;
        ProductId = productId;
        Name = name;
    }
}
