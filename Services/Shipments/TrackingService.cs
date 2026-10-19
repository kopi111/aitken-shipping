using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

public class TrackingService : ITrackingService
{
    private readonly IShipmentRepository repository;

    public TrackingService(IShipmentRepository repository) => this.repository = repository;

    public TrackingResult Track(string trackingNumber)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
        {
            return TrackingResult.ForMissing(string.Empty);
        }

        string normalized = trackingNumber.Trim().ToUpperInvariant();

        return repository.TryGet(normalized, out Shipment shipment)
            ? TrackingResult.ForFound(shipment)
            : TrackingResult.ForMissing(normalized);
    }
}
