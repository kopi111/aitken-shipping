using System.ComponentModel.DataAnnotations;

namespace AitkenShipping.Models;

public class QuoteRequest
{
    [Required]
    public string OriginParish { get; set; } = string.Empty;

    [Required]
    public string DestinationParish { get; set; } = string.Empty;

    [Range(0.1, 1000, ErrorMessage = "Weight must be between 0.1 and 1000 kg.")]
    public decimal WeightKg { get; set; }

    [Range(0, 300, ErrorMessage = "Length must be between 0 and 300 cm.")]
    public int LengthCm { get; set; }

    [Range(0, 300, ErrorMessage = "Width must be between 0 and 300 cm.")]
    public int WidthCm { get; set; }

    [Range(0, 300, ErrorMessage = "Height must be between 0 and 300 cm.")]
    public int HeightCm { get; set; }

    public ServiceLevel Service { get; set; } = ServiceLevel.Standard;

    public bool IsFragile { get; set; }

    [Range(0, 5_000_000, ErrorMessage = "Declared value looks out of range.")]
    public decimal DeclaredValueJmd { get; set; }
}
