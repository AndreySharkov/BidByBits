# Bid By Bits

Web application for online auctions and selling of collectibles and second-hand items in Bulgaria.

## Overview

Bid By Bits bridges the gap between traditional second-hand trading and modern e-commerce security. Built on .NET 10 and ASP.NET MVC, it provides a transparent and accountable marketplace for both casual sellers and dedicated collectors.

### Two Selling Options
- **Auction** — Set a date, start price, and let buyers bid in real-time
- **Fixed Price / Best Offer** — Sell at a set price or accept the best offer

### Buyer & Seller Protection
- **Credibility Scoring** — Buyers see seller ratings (1-5 stars) before purchasing
- **Delivery Tracking** — Every order is tracked: paid, shipped, delivered, or refused
- **Refusal Logging** — If a buyer refuses delivery, it's logged and affects their reputation
- **Proof Submission** — Buyers can submit proof of return delivery to resolve disputes

## Tech Stack

- **Backend:** C# / .NET 10 / ASP.NET Core MVC
- **Data:** Entity Framework Core + SQLite (dev) / SQL Server (production)
- **Frontend:** Razor Views + Bootstrap 5
- **Version Control:** Git + GitHub

## Planned Features

| Feature | Status |
|---------|--------|
| User registration & authentication | Planned |
| Item listing (auction & fixed price) | Planned |
| Bidding system | Planned |
| Order & delivery tracking | Planned |
| Seller credibility ratings | Planned |
| Refusal logging & proof submission | Planned |
| Admin panel | Phase 2 |

## Project Structure

```
BidByBits/
├── BidByBits.Data/          # Domain models, DbContext, migrations
│   └── Models/              # User, Item, Bid, Order, Rating, etc.
├── BidByBits/               # Web Application (MVC)
│   ├── Controllers/
│   ├── Views/
│   └── wwwroot/
└── docs/                    # Documentation
    ├── DB_SCHEMA.md         # Database design
    └── PROJECT_DOCS.md      # Full project documentation
```

## Getting Started

```bash
# Restore packages
dotnet restore

# Build
dotnet build

# Run (creates SQLite database automatically)
dotnet run --project BidByBits/BidByBits.csproj
```

## Documentation & Links

- **GitHub Repository:** https://github.com/AndreySharkov/BidByBits
- **Full Project Docs:** [docs/PROJECT_DOCS.md](docs/PROJECT_DOCS.md)
- **Database Schema:** [docs/DB_SCHEMA.md](docs/DB_SCHEMA.md)

## License

Private — Diploma project.
