using AitkenShipping.Models;
using AitkenShipping.Models.ViewModels;
using AitkenShipping.Services.Shipments;
using Microsoft.AspNetCore.Mvc;

namespace AitkenShipping.Controllers;

public class TrackController : Controller
{
    private readonly ITrackingService trackingService;

    public TrackController(ITrackingService trackingService) => this.trackingService = trackingService;

    [HttpGet]
    public IActionResult Index(string? trackingNumber)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
        {
            return View(new TrackPageViewModel());
        }

        return View(ResultPage(trackingNumber));
    }

    private TrackPageViewModel ResultPage(string trackingNumber)
    {
        TrackingResult result = trackingService.Track(trackingNumber);
        return new TrackPageViewModel
        {
            TrackingNumber = trackingNumber,
            Result = result,
            Searched = true
        };
    }
}
