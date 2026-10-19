using AitkenShipping.Models;

namespace AitkenShipping.Services.Pricing;

/// <summary>
/// The published rate card for each service level: a flat base fee, a
/// per-kilogram rate, and the promised transit time in business days.
/// </summary>
public class ServiceTariff
{
    public decimal BaseFeeJmd { get; init; }
    public decimal PerKilogramJmd { get; init; }
    public int TransitBusinessDays { get; init; }

    private static readonly Dictionary<ServiceLevel, ServiceTariff> RateCard = new()
    {
        [ServiceLevel.Economy] = new ServiceTariff { BaseFeeJmd = 600m, PerKilogramJmd = 120m, TransitBusinessDays = 5 },
        [ServiceLevel.Standard] = new ServiceTariff { BaseFeeJmd = 900m, PerKilogramJmd = 150m, TransitBusinessDays = 3 },
        [ServiceLevel.Express] = new ServiceTariff { BaseFeeJmd = 1600m, PerKilogramJmd = 240m, TransitBusinessDays = 1 },
        [ServiceLevel.Overnight] = new ServiceTariff { BaseFeeJmd = 2800m, PerKilogramJmd = 380m, TransitBusinessDays = 1 }
    };

    public static ServiceTariff For(ServiceLevel service) => RateCard[service];
}
