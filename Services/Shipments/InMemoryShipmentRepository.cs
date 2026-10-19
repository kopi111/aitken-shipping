using System.Collections.Concurrent;
using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

public class InMemoryShipmentRepository : IShipmentRepository
{
    private readonly ConcurrentDictionary<string, Shipment> shipmentsByTrackingNumber = new(StringComparer.OrdinalIgnoreCase);

    public InMemoryShipmentRepository()
    {
        foreach (Shipment shipment in ShipmentSeeder.Sample())
        {
            Add(shipment);
        }
    }

    public bool TryGet(string trackingNumber, out Shipment shipment)
    {
        if (trackingNumber is null)
        {
            shipment = null!;
            return false;
        }

        return shipmentsByTrackingNumber.TryGetValue(trackingNumber.Trim(), out shipment!);
    }

    public void Add(Shipment shipment) =>
        shipmentsByTrackingNumber[shipment.TrackingNumber] = shipment;

    public IReadOnlyCollection<Shipment> All() =>
        shipmentsByTrackingNumber.Values.ToList();

    public int Count() => shipmentsByTrackingNumber.Count;
}
