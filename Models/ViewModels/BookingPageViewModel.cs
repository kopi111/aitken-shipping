namespace AitkenShipping.Models.ViewModels;

public class BookingPageViewModel
{
    public BookingRequest Request { get; set; } = new();
    public Shipment? Confirmed { get; set; }
    public IReadOnlyCollection<string> Parishes { get; set; } = new List<string>();
}
