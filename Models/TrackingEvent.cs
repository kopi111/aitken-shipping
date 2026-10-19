namespace AitkenShipping.Models;

public class TrackingEvent
{
    public DateTime OccurredAt { get; init; }
    public ShipmentStatus Status { get; init; }
    public string Location { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
