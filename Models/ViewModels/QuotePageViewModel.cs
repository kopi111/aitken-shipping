namespace AitkenShipping.Models.ViewModels;

public class QuotePageViewModel
{
    public QuoteRequest Request { get; set; } = new();
    public QuoteResult? Result { get; set; }
    public IReadOnlyCollection<string> Parishes { get; set; } = new List<string>();
}
