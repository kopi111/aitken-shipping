using System.ComponentModel.DataAnnotations;

namespace AitkenShipping.Models;

public class BookingRequest
{
    [Required, StringLength(80)]
    public string SenderName { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string RecipientName { get; set; } = string.Empty;

    [Required]
    public string OriginParish { get; set; } = string.Empty;

    [Required]
    public string DestinationParish { get; set; } = string.Empty;

    public ServiceLevel Service { get; set; } = ServiceLevel.Standard;

    [Range(0.1, 1000, ErrorMessage = "Weight must be between 0.1 and 1000 kg.")]
    public decimal WeightKg { get; set; }

    public bool IsFragile { get; set; }
}
