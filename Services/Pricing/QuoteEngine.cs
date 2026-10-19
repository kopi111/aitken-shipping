using AitkenShipping.Models;

namespace AitkenShipping.Services.Pricing;

/// <summary>
/// Builds a transparent, itemised shipping quote from a request. Every charge
/// is exposed as a line item so customers can see exactly what they pay for.
/// </summary>
public class QuoteEngine : IQuoteEngine
{
    private const decimal DimensionalDivisor = 5000m;
    private const decimal FragileSurchargeRate = 0.12m;
    private const decimal InsuranceRate = 0.015m;

    private readonly DistanceZone distanceZone;

    public QuoteEngine(DistanceZone distanceZone) => this.distanceZone = distanceZone;

    public QuoteResult Quote(QuoteRequest request)
    {
        GuardParishesAreKnown(request);

        ServiceTariff tariff = ServiceTariff.For(request.Service);
        decimal chargeableWeight = ChargeableWeight(request);
        decimal distanceMultiplier = distanceZone.MultiplierFor(request.OriginParish, request.DestinationParish);

        List<QuoteLineItem> lineItems = BuildLineItems(request, tariff, chargeableWeight, distanceMultiplier);

        return new QuoteResult
        {
            Service = request.Service,
            ChargeableWeightKg = Math.Round(chargeableWeight, 2),
            TotalJmd = Math.Round(lineItems.Sum(item => item.AmountJmd), 2),
            TransitBusinessDays = tariff.TransitBusinessDays,
            LineItems = lineItems
        };
    }

    private static void GuardParishesAreKnown(QuoteRequest request)
    {
        if (!ParishDirectory.IsKnown(request.OriginParish))
        {
            throw new ArgumentException($"We do not service the origin parish: {request.OriginParish}");
        }

        if (!ParishDirectory.IsKnown(request.DestinationParish))
        {
            throw new ArgumentException($"We do not service the destination parish: {request.DestinationParish}");
        }
    }

    private List<QuoteLineItem> BuildLineItems(
        QuoteRequest request,
        ServiceTariff tariff,
        decimal chargeableWeight,
        decimal distanceMultiplier)
    {
        var items = new List<QuoteLineItem>
        {
            new() { Label = $"{request.Service} base fee", AmountJmd = tariff.BaseFeeJmd },
            new() { Label = $"Weight ({chargeableWeight:0.##} kg × ${tariff.PerKilogramJmd:0})", AmountJmd = WeightCharge(tariff, chargeableWeight) },
            new() { Label = $"Distance ({request.OriginParish} → {request.DestinationParish})", AmountJmd = DistanceCharge(tariff, chargeableWeight, distanceMultiplier) }
        };

        if (request.IsFragile)
        {
            items.Add(new QuoteLineItem { Label = "Fragile handling", AmountJmd = FragileSurcharge(items) });
        }

        if (request.DeclaredValueJmd > 0)
        {
            items.Add(new QuoteLineItem { Label = "Insurance (1.5% of value)", AmountJmd = InsuranceCharge(request) });
        }

        return items.Select(RoundedItem).ToList();
    }

    private static decimal WeightCharge(ServiceTariff tariff, decimal chargeableWeight) =>
        tariff.PerKilogramJmd * chargeableWeight;

    private static decimal DistanceCharge(ServiceTariff tariff, decimal chargeableWeight, decimal distanceMultiplier)
    {
        decimal transportBase = tariff.BaseFeeJmd + (tariff.PerKilogramJmd * chargeableWeight);
        return transportBase * (distanceMultiplier - 1.0m);
    }

    private static decimal FragileSurcharge(IEnumerable<QuoteLineItem> current) =>
        current.Sum(item => item.AmountJmd) * FragileSurchargeRate;

    private static decimal InsuranceCharge(QuoteRequest request) =>
        request.DeclaredValueJmd * InsuranceRate;

    private static decimal ChargeableWeight(QuoteRequest request) =>
        Math.Max(request.WeightKg, DimensionalWeight(request));

    private static decimal DimensionalWeight(QuoteRequest request) =>
        (request.LengthCm * request.WidthCm * request.HeightCm) / DimensionalDivisor;

    private static QuoteLineItem RoundedItem(QuoteLineItem item) =>
        new() { Label = item.Label, AmountJmd = Math.Round(item.AmountJmd, 2) };
}
