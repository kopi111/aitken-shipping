namespace AitkenShipping.Services.Shipments;

/// <summary>
/// Issues unique, human-readable tracking numbers (AS + six digits + JM).
/// Sequence starts above the seeded demo range to avoid collisions.
/// </summary>
public class TrackingNumberFactory
{
    private int sequence = 240100;

    public string Next()
    {
        int value = Interlocked.Increment(ref sequence);
        return $"AS{value:D6}JM";
    }
}
