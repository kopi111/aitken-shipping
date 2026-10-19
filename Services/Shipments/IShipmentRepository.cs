using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

public interface IShipmentRepository
{
    bool TryGet(string trackingNumber, out Shipment shipment);
    void Add(Shipment shipment);
    IReadOnlyCollection<Shipment> All();
    int Count();
}
