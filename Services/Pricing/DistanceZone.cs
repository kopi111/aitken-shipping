namespace AitkenShipping.Services.Pricing;

/// <summary>
/// Translates an origin/destination parish pair into a distance multiplier.
/// Same parish ships cheapest; crossing the island costs the most.
/// </summary>
public class DistanceZone
{
    public decimal MultiplierFor(string originParish, string destinationParish)
    {
        if (originParish.Equals(destinationParish, StringComparison.OrdinalIgnoreCase))
        {
            return LocalMultiplier;
        }

        int regionGap = Math.Abs(ParishDirectory.RegionOf(originParish) - ParishDirectory.RegionOf(destinationParish));
        return BaseMultiplier + (PerRegionStep * regionGap);
    }

    private const decimal LocalMultiplier = 0.95m;
    private const decimal BaseMultiplier = 1.0m;
    private const decimal PerRegionStep = 0.18m;
}
