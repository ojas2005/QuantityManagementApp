<div align="center">

# Quantity Measurement

**A unit-measurement system built incrementally across 17 use cases, test-first.**

[![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![NUnit](https://img.shields.io/badge/NUnit-tested-22C55E?style=flat-square)](https://nunit.org)

[Frontend repo](https://github.com/ojas2005/QuantityManagementApp_Frontend) · [Live demo](https://quantitymanagementapp-frontend.onrender.com)

</div>

---

## What this is

A measurement-comparison and conversion engine, grown one use case at a time from a single
equality check into an N-tier API with persistence and OAuth. Each folder under `feature/`
is a self-contained step with its own tests, so the commit history doubles as a record of
how the design evolved.

The interesting part is not the final app, it is the refactoring path: watching a
`FeetEquality` class turn into a generic `IUnit` abstraction that handles length, weight,
volume and temperature without a switch statement in sight.

## The progression

| Step | Use case | What it introduced |
|---|---|---|
| UC1–UC2 | Feet / inch equality | Value-object equality, first tests |
| UC3 | Generic length | Extracting the abstraction |
| UC4–UC5 | Yard, conversion | Conversion factors as data, not branches |
| UC6–UC8 | Addition, target units | Operations across mixed units |
| UC9–UC11 | Weight, volume | Generalising beyond length via `IUnit` |
| UC12–UC13 | Subtraction, division, DRY pass | Removing duplication across operations |
| UC14 | Temperature | Non-linear conversion (offset, not just scale) |
| **UC15** | **N-tier architecture** | Model / Repository / Business / Application split |
| UC16 | Database integration | EF Core + SQL Server persistence |
| UC18 | Google auth | JWT + Google OAuth 2.0, BCrypt hashing |

## Architecture (UC15 onward)

```
   ApplicationLayer     ← controllers, DI wiring, HTTP concerns
          │
   BusinessLayer        ← conversion rules, validation, orchestration
          │
   RepositoryLayer      ← EF Core data access, no business logic
          │
   ModelLayer           ← entities and DTOs, referenced by all layers
```

Dependencies point one direction only. `ModelLayer` knows about nobody; `ApplicationLayer`
knows about everybody. That makes the business rules unit-testable without a database.

## Tech stack

| Concern | Choice |
|---|---|
| Runtime | .NET 8 · ASP.NET Core |
| Data | EF Core 8 · SQL Server |
| Caching | Redis (`StackExchange.Redis`) · response caching |
| Auth | JWT bearer · Google OAuth 2.0 · BCrypt password hashing |
| Docs | Swagger / Swashbuckle |
| Tests | NUnit — 43 tests across the N-tier build |

## Running it

```bash
git clone https://github.com/ojas2005/QuantityManagementApp.git
cd QuantityManagementApp/feature/UC15-NtierArchitechture

dotnet restore
dotnet build
dotnet test                       # 43 tests
dotnet run --project ApplicationLayer
```

Swagger UI is served at the app root once running. For the persistence and auth steps, use
`UC16-DatabaseIntegration` or `UC18-GoogleAuthIntegration` and set the connection string in
`appsettings.Development.json`.

## Author

**Ojas Tiwari** — [GitHub](https://github.com/ojas2005) · [LinkedIn](https://linkedin.com/in/ojas-tiwari-122402253)
