using AitkenShipping.Models;

namespace AitkenShipping.Services.Shipments;

/// <summary>
/// Provides demo shipments with known tracking numbers so the tracking page
/// works out of the box. Try AS240001JM, AS240002JM, or AS240003JM.
/// </summary>
public static class ShipmentSeeder
{
    public static IEnumerable<Shipment> Sample()
    {
        DateTime today = DateTime.Today;

        yield return InTransitParcel(today);
        yield return DeliveredParcel(today);
        yield return OutForDeliveryParcel(today);
    }

    private static Shipment InTransitParcel(DateTime today)
    {
        var shipment = new Shipment
        {
            TrackingNumber = "AS240001JM",
            SenderName = "Blue Mountain Coffee Co.",
            RecipientName = "Island Roasters",
            OriginParish = "Kingston",
            DestinationParish = "Saint James",
            Service = ServiceLevel.Express,
            WeightKg = 4.5m,
            PriceJmd = 3480m,
            BookedOn = today.AddDays(-1),
            EstimatedDelivery = today.AddDays(1)
        };

        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(9), Status = ShipmentStatus.Booked, Location = "Kingston", Description = "Shipment booked and label created." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(14), Status = ShipmentStatus.PickedUp, Location = "Kingston Hub", Description = "Picked up by courier." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddHours(6), Status = ShipmentStatus.InTransit, Location = "Mandeville Sort Facility", Description = "Departed central sort facility." });
        return shipment;
    }

    private static Shipment DeliveredParcel(DateTime today)
    {
        var shipment = new Shipment
        {
            TrackingNumber = "AS240002JM",
            SenderName = "Portmore Electronics",
            RecipientName = "A. Campbell",
            OriginParish = "Saint Catherine",
            DestinationParish = "Saint Ann",
            Service = ServiceLevel.Standard,
            WeightKg = 1.2m,
            PriceJmd = 1290m,
            BookedOn = today.AddDays(-4),
            EstimatedDelivery = today.AddDays(-1)
        };

        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-4).AddHours(11), Status = ShipmentStatus.Booked, Location = "Spanish Town", Description = "Shipment booked." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-3).AddHours(10), Status = ShipmentStatus.InTransit, Location = "Spanish Town Hub", Description = "In transit to destination parish." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(9), Status = ShipmentStatus.OutForDelivery, Location = "Ocho Rios Depot", Description = "Out for delivery." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(13), Status = ShipmentStatus.Delivered, Location = "Ocho Rios", Description = "Delivered. Signed for by A. Campbell." });
        return shipment;
    }

    private static Shipment OutForDeliveryParcel(DateTime today)
    {
        var shipment = new Shipment
        {
            TrackingNumber = "AS240003JM",
            SenderName = "Yaad Threads",
            RecipientName = "D. Aitken",
            OriginParish = "Manchester",
            DestinationParish = "Kingston",
            Service = ServiceLevel.Overnight,
            WeightKg = 0.8m,
            PriceJmd = 3104m,
            BookedOn = today.AddDays(-1),
            EstimatedDelivery = today
        };

        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(16), Status = ShipmentStatus.Booked, Location = "Mandeville", Description = "Overnight shipment booked." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddDays(-1).AddHours(19), Status = ShipmentStatus.InTransit, Location = "Mandeville Hub", Description = "On the overnight line-haul to Kingston." });
        shipment.RecordEvent(new TrackingEvent { OccurredAt = today.AddHours(7), Status = ShipmentStatus.OutForDelivery, Location = "Kingston Depot", Description = "Out for delivery this morning." });
        return shipment;
    }
}
