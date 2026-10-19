using AitkenShipping.Models;
using AitkenShipping.Models.ViewModels;
using AitkenShipping.Services.Pricing;
using AitkenShipping.Services.Shipments;
using Microsoft.AspNetCore.Mvc;

namespace AitkenShipping.Controllers;

public class BookController : Controller
{
    private readonly IBookingService bookingService;

    public BookController(IBookingService bookingService) => this.bookingService = bookingService;

    [HttpGet]
    public IActionResult Index() => View(EmptyPage());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(BookingRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(PageFor(request));
        }

        try
        {
            Shipment confirmed = bookingService.Book(request);
            return View("Confirmation", confirmed);
        }
        catch (ArgumentException error)
        {
            ModelState.AddModelError(string.Empty, error.Message);
            return View(PageFor(request));
        }
    }

    private static BookingPageViewModel EmptyPage() => PageFor(new BookingRequest());

    private static BookingPageViewModel PageFor(BookingRequest request) => new()
    {
        Request = request,
        Parishes = ParishDirectory.All.OrderBy(name => name).ToList()
    };
}
