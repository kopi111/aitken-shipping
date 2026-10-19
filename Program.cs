using AitkenShipping.Services.Pricing;
using AitkenShipping.Services.Shipments;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IShipmentRepository, InMemoryShipmentRepository>();
builder.Services.AddSingleton<TrackingNumberFactory>();
builder.Services.AddScoped<DistanceZone>();
builder.Services.AddScoped<IQuoteEngine, QuoteEngine>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
