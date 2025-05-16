# Inventory — full-stack stationery shop demo  
> Browse, add, and manage books, paper, and writing tools end-to-end.

## 🛠 Tech Stack & Skills Demonstrated
| Area | Stack / Library | Highlights shown in this project |
|------|-----------------|-----------------------------------|
| Front-end | **ASP.NET Core Razor Pages**, Bootstrap 5 | Dynamic forms, modals, DI-switchable mock vs. live services |
| Back-end core | **C# 10**, Dapper, SQL Server LocalDB | Clean architecture, repository pattern, factory resolvers |
| Logging | **Serilog** | Multi-file per-run logs (trace → fatal) via shared configurator |
| Testing | **xUnit**, Bunit, Moq | Unit & integration tests for DAL and Razor Pages |
| Dev & Ops | .NET CLI, GitHub Actions | Restore/build/test CI pipeline, conventional commits |

## ✨ What the App Does

### Core Functionality
* Product catalog with type filters (Books 📚, Paper 📄, Writing 🖊️).  
* Create products via rich, type-specific form and preview-before-save.  
* (Optional) Order flow backed by mock or live API.

### Visuals & UX
* Responsive Bootstrap grid & cards.  
* Modal dialogs for detail drill-down and confirmation.  
* Friendly type labels, badges, and input validation.

### Engineering Features
* Layered **API → Core → Frontend** solution; each project test-covered.  
* Repository + factory pattern for polymorphic `Product` hierarchy.  
* **Serilog** logger auto-creates `Logs/<Project>/<RunTimestamp>/` with leveled files.  
* **HttpClient factory** & DI toggle between mocks and live endpoints.  
* Singleton database connection helper that can be swapped with test doubles.

## 🚀 Running Locally

> **Prerequisites:** .NET 8 SDK, local SQL Server Express/LocalDB  
> *(Optional)* Mobile experiments: `dotnet workload install maui`

```bash
git clone https://github.com/<your-user>/Inventory.git
cd Inventory

# Restore & build everything
dotnet restore
dotnet build

# 1️⃣  Start the Web API (default https://localhost:5001)
dotnet run --project Backend/Inventory.API

# 2️⃣  In a new terminal, launch the Razor Pages front-end
dotnet run --project Frontend/Inventory.Frontend

# Open https://localhost:5002 in your browser
