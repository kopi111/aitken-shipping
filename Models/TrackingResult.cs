namespace AitkenShipping.Models;

/// <summary>
/// Special-case result for a tracking lookup so callers never handle a null
/// shipment. <see cref="Found"/> tells the caller whether a match exists.
/// </summary>
public class TrackingResult
{
    public bool Found { get; private init; }
    public string TrackingNumber { get; private init; } = string.Empty;
    public Shipment? Shipment { get; private init; }

    public static TrackingResult ForFound(Shipment shipment) => new()
    {
        Found = true,
        TrackingNumber = shipment.TrackingNumber,
        Shipment = shipment
    };

    public static TrackingResult ForMissing(string trackingNumber) => new()
    {
        Found = false,
        TrackingNumber = trackingNumber
    };
}
