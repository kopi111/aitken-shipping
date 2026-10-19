using AitkenShipping.Models;
using AitkenShipping.Services.Pricing;

namespace AitkenShipping.Services.Shipments;

public class BookingService : IBookingService
{
    private readonly IShipmentRepository repository;
    private readonly IQuoteEngine quoteEngine;
    private readonly TrackingNumberFactory trackingNumbers;

    public BookingService(
        IShipmentRepository repository,
        IQuoteEngine quoteEngine,
        TrackingNumberFactory trackingNumbers)
    {
        this.repository = repository;
        this.quoteEngine = quoteEngine;
        this.trackingNumbers = trackingNumbers;
    }

    public Shipment Book(BookingRequest request)
    {
        QuoteResult quote = quoteEngine.Quote(ToQuoteRequest(request));
        Shipment shipment = CreateShipment(request, quote);
        shipment.RecordEvent(BookingEvent(shipment));
        repository.Add(shipment);
        return shipment;
    }

    private static QuoteRequest ToQuoteRequest(BookingRequest request) => new()
    {
        OriginParish = request.OriginParish,
        DestinationParish = request.DestinationParish,
        WeightKg = request.WeightKg,
        Service = request.Service,
        IsFragile = request.IsFragile
    };

    private Shipment CreateShipment(BookingRequest request, QuoteResult quote)
    {
        DateTime bookedOn = DateTime.Now;
        return new Shipment
        {
            TrackingNumber = trackingNumbers.Next(),
            SenderName = request.SenderName,
            RecipientName = request.RecipientName,
            OriginParish = request.OriginParish,
            DestinationParish = request.DestinationParish,
            Service = request.Service,
            WeightKg = request.WeightKg,
            PriceJmd = quote.TotalJmd,
            BookedOn = bookedOn,
            EstimatedDelivery = BusinessCalendar.AddBusinessDays(bookedOn.Date, quote.TransitBusinessDays)
        };
    }

    private static TrackingEvent BookingEvent(Shipment shipment) => new()
    {
        OccurredAt = shipment.BookedOn,
        Status = ShipmentStatus.Booked,
        Location = shipment.OriginParish,
        Description = "Shipment booked and label created."
    };
}
