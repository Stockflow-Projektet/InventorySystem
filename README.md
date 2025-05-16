# Inventory — full-stack stationery shop demo  
> Browse, add, and manage books, paper, and writing tools end-to-end.

## 🛠 Tech Stack & Skills Demonstrated - By Johan Hoppe Rauer
| Area | Stack / Library | Highlights shown in this project |
|------|-----------------|-----------------------------------|
| Front-end UI | **ASP.NET Core Razor Pages**, Bootstrap 5 | Dynamic forms, modal dialogs, DI-switchable services |
| Front-end Scripting | **JavaScript/ES6** | Client-side type-aware field toggling & preview modal |
| API Layer | **ASP.NET Core Minimal API** | RESTful endpoints (`/api/products`, `/api/orders`), Swagger |
| Core Domain | **C# 10**, Dapper | Repository pattern, factory resolver, polymorphic models |
| Database | **SQL Server LocalDB** | Singleton connection factory, script-free Dapper migrations |
| Logging | **Serilog** | Per-run multi-file logs (trace → fatal) via central configurator |
| Testing | **xUnit**, Bunit, Moq | DAL integration tests, Razor Page unit tests, mock HttpClient |
| CI / DevOps | **GitHub Actions**, .NET CLI | Restore → build → test pipeline, artifact logs |
| Patterns & Design | Repository, Factory, Singleton | Clean separation of concerns, easy mocking |

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
