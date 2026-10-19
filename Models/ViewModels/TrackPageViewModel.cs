namespace AitkenShipping.Models.ViewModels;

public class TrackPageViewModel
{
    public string TrackingNumber { get; set; } = string.Empty;
    public TrackingResult? Result { get; set; }
    public bool Searched { get; set; }
}
