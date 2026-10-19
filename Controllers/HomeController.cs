using System.Diagnostics;
using AitkenShipping.Models;
using AitkenShipping.Services.Shipments;
using Microsoft.AspNetCore.Mvc;

namespace AitkenShipping.Controllers;

public class HomeController : Controller
{
    private readonly IShipmentRepository repository;

    public HomeController(IShipmentRepository repository) => this.repository = repository;

    public IActionResult Index()
    {
        ViewData["ShipmentsTracked"] = repository.Count();
        return View();
    }

    public IActionResult Services() => View();

    public IActionResult About() => View();

    public IActionResult Contact() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
