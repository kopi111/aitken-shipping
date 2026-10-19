namespace AitkenShipping.Services.Pricing;

/// <summary>
/// The fourteen parishes of Jamaica, grouped into shipping regions so the
/// pricing engine can reason about distance without external map data.
/// </summary>
public static class ParishDirectory
{
    private static readonly Dictionary<string, int> RegionByParish = new(StringComparer.OrdinalIgnoreCase)
    {
        // Region 0 — West
        ["Hanover"] = 0,
        ["Westmoreland"] = 0,
        ["Saint James"] = 0,
        ["Trelawny"] = 0,
        // Region 1 — Central
        ["Saint Elizabeth"] = 1,
        ["Manchester"] = 1,
        ["Clarendon"] = 1,
        ["Saint Ann"] = 1,
        // Region 2 — East
        ["Saint Catherine"] = 2,
        ["Kingston"] = 2,
        ["Saint Andrew"] = 2,
        ["Saint Thomas"] = 2,
        ["Portland"] = 2,
        ["Saint Mary"] = 2
    };

    public static IReadOnlyCollection<string> All => RegionByParish.Keys;

    public static bool IsKnown(string parish) => RegionByParish.ContainsKey(parish);

    public static int RegionOf(string parish)
    {
        if (RegionByParish.TryGetValue(parish, out int region))
        {
            return region;
        }

        throw new ArgumentException($"Unknown parish: {parish}", nameof(parish));
    }
}
