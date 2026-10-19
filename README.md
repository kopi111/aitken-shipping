# Aitken Shipping

A shipping-company website with real functionality — instant quotes, live shipment
tracking, and booking — built with ASP.NET Core 8 MVC. Jamaica-parish–aware pricing,
in-memory data (seeded), custom design system, no external CSS/JS dependencies.

Built to test-drive the **Aitken Software Company** agent team
(Rapid Prototyper → Backend Architect → AI/pricing → UI Designer → Whimsy Injector →
Reality Checker).

## Run

```bash
dotnet run
# then open the URL it prints (e.g. http://localhost:5274)
```

## Features

| Area | Route | What it does |
|------|-------|--------------|
| Home | `/` | Marketing landing + quick-track box + live parcel count |
| Services | `/Home/Services` | Rate card for Economy / Standard / Express / Overnight |
| Get a Quote | `/Quote` | Itemised pricing engine (weight, dimensional weight, distance, fragile, insurance) |
| Track | `/Track?trackingNumber=AS240001JM` | Real-time status + full journey timeline |
| Book | `/Book` | Creates a shipment, issues a tracking number, prices it |
| About / Contact | `/Home/About`, `/Home/Contact` | Company pages |

### Demo tracking numbers (seeded)
- `AS240001JM` — In transit (Kingston → Saint James, Express)
- `AS240002JM` — Delivered (Saint Catherine → Saint Ann, Standard)
- `AS240003JM` — Out for delivery (Manchester → Kingston, Overnight)

## Pricing model

Total = base fee + weight charge + distance charge (+ fragile 12%) (+ insurance 1.5%).

- **Chargeable weight** = max(actual kg, dimensional weight where dim = L×W×H ÷ 5000).
- **Distance** = Jamaica parishes grouped into West / Central / East regions;
  same-parish is discounted, crossing the island costs the most.
- **Transit** = business days per service level, weekends skipped for the ETA.

## Design notes

- Domain, pricing, and shipment services are split into small, single-responsibility
  classes with interfaces + DI (Clean Code standards).
- No null returns: tracking uses a `TrackingResult` special-case object and the
  repository exposes `TryGet`.
- Self-contained UI: one hand-written `wwwroot/css/site.css`, system font stack,
  no CDN / Bootstrap / jQuery (client validation partial aside).

## Verified working (Reality Checker pass)

All routes return 200; known/unknown tracking, quote POST (math verified), and booking
POST (issues a number that is immediately trackable) all exercised over HTTP.
Invalid input re-renders with validation instead of crashing.
