using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

public interface IBookingService
{
    Shipment Book(BookingRequest request);
}
