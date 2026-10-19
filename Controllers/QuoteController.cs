using AitkenShipping.Models;
using AitkenShipping.Models.ViewModels;
using AitkenShipping.Services.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace AitkenShipping.Controllers;

public class QuoteController : Controller
{
    private readonly IQuoteEngine quoteEngine;

    public QuoteController(IQuoteEngine quoteEngine) => this.quoteEngine = quoteEngine;

    [HttpGet]
    public IActionResult Index() => View(EmptyPage());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(QuoteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(PageFor(request));
        }

        try
        {
            QuoteResult result = quoteEngine.Quote(request);
            return View(PageFor(request, result));
        }
        catch (ArgumentException error)
        {
            ModelState.AddModelError(string.Empty, error.Message);
            return View(PageFor(request));
        }
    }

    private static QuotePageViewModel EmptyPage() => PageFor(new QuoteRequest());

    private static QuotePageViewModel PageFor(QuoteRequest request, QuoteResult? result = null) => new()
    {
        Request = request,
        Result = result,
        Parishes = OrderedParishes()
    };

    private static IReadOnlyCollection<string> OrderedParishes() =>
        ParishDirectory.All.OrderBy(name => name).ToList();
}
