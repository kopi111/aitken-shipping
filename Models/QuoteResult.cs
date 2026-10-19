namespace AitkenShipping.Models;

public class QuoteLineItem
{
    public string Label { get; init; } = string.Empty;
    public decimal AmountJmd { get; init; }
}

public class QuoteResult
{
    public ServiceLevel Service { get; init; }
    public decimal ChargeableWeightKg { get; init; }
    public decimal TotalJmd { get; init; }
    public int TransitBusinessDays { get; init; }
    public IReadOnlyList<QuoteLineItem> LineItems { get; init; } = new List<QuoteLineItem>();
}
