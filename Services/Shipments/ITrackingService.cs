using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

public interface ITrackingService
{
    TrackingResult Track(string trackingNumber);
}
