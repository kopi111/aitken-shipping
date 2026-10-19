namespace AitkenShipping.Models;

public class Shipment
{
    public string TrackingNumber { get; init; } = string.Empty;
    public string SenderName { get; init; } = string.Empty;
    public string RecipientName { get; init; } = string.Empty;
    public string OriginParish { get; init; } = string.Empty;
    public string DestinationParish { get; init; } = string.Empty;
    public ServiceLevel Service { get; init; }
    public decimal WeightKg { get; init; }
    public decimal PriceJmd { get; init; }
    public DateTime BookedOn { get; init; }
    public DateTime EstimatedDelivery { get; init; }

    private readonly List<TrackingEvent> history = new();

    public IReadOnlyList<TrackingEvent> History => history;

    public ShipmentStatus CurrentStatus =>
        history.Count == 0 ? ShipmentStatus.Booked : history[^1].Status;

    public void RecordEvent(TrackingEvent trackingEvent) => history.Add(trackingEvent);
}
