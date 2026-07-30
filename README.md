# Library Management System (LMS)

A modern, robust web application built using **ASP.NET Core (Razor MVC)** on **.NET 10.0** and **Entity Framework Core**. The application is designed to digitize and automate core library workflows, including books inventory cataloging, student and librarian member records, and loan transactions (borrowing and returning books).

---

## 🚀 Key Features

- 📚 **Book Catalog CRUD**: Add, edit, inspect, and remove book listings.
- 🔍 **Search & Pagination**: Browse the library catalog with a responsive pagination system (5 books per page) and search by Title, Author, or ISBN.
- 🔄 **Borrow/Return Transactions**: Loan books to registered borrowers with active status tracking, date loggings, and availability toggles.
- 📊 **Analytical Dashboard**: Live counts of total books, registered students, active librarians, and currently borrowed copies.
- 📰 **Periodicals Section**: In-memory directories for Magazines and daily Newspapers.
- ⚙️ **Auto-Migrations**: Programmatic scaffolding and execution of Entity Framework Core schema migrations at startup in development.
- 🧪 **xUnit Test Suite**: Automated unit tests mocking database operations via the EF Core InMemory provider and using FluentAssertions for expressive validation.

---

## 🛠️ Technology Stack

- **Framework**: C# .NET 10.0 (ASP.NET Core Razor MVC)
- **Object-Relational Mapper (ORM)**: Entity Framework Core
- **Database**: SQL Server LocalDB (Development) / SQL Server Express
- **Testing Tools**: xUnit, FluentAssertions, EF Core InMemory Provider
- **Frontend Styling**: Bootstrap, HTML5, CSS3

---

## 📁 Repository Layout

- `/LMS/`: Main Web Application project.
  - `Controllers/`: Controllers handling routing and business request logic (`BooksController`, `BorrowController`, `DashboardController`, `LoginController`, etc.).
  - `Models/`: Data entities and database representation schemas (`Book.cs`, `BorrowRecord.cs`, `LibraryContext.cs`, etc.).
  - `Views/`: CSHTML Razor views for dynamic UI rendering.
  - `Migrations/`: Database schema version snapshots.
  - `Program.cs`: Service registrations, request middlewares pipeline, and auto-migration runner.
  - `appsettings.json`: Configuration keys (Default Connection String).
- `/TestProject1/`: xUnit test project containing unit tests for `BooksController`.
- `LMS_Blackbook_Report.docx`: The academic capstone project report.
- `LMS_Blackbook_Report.pdf`: Ready-to-print project report document.

---

## 💻 Local Setup & Installation

### Prerequisites
- **.NET 10 SDK**
- **SQL Server LocalDB** (usually installed with Visual Studio)
- **dotnet-ef CLI tool**: Install globally via:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Step-by-Step Run
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/Snorlax231155/LMS.git
   cd LMS
   ```
2. **Database Connection Configuration**:
   Open `LMS/appsettings.json` and ensure the connection string points to your local SQL Server instance:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LMS;Integrated Security=True;Encrypt=False"
   }
   ```
3. **Database Migration and Schema Scaffolding**:
   If you want to apply migrations manually before running:
   ```bash
   dotnet ef database update -p LMS -s LMS
   ```
   *(Note: Program.cs will also attempt to automatically create and apply migrations if run in a Development environment).*
4. **Compile and Run**:
   ```bash
   dotnet run --project LMS
   ```
5. **Access the Portal**:
   Open your browser and navigate to `http://localhost:5000` (or the console output URL).

---

## 🧪 Running Unit Tests

The test project validates catalog pagination bounds, keyword search filtering, and details status results under simulated database scenarios.

To execute the test suite, run the following command from the repository root:
```bash
dotnet test TestProject1/TestProject1.csproj
```

---

## 👤 Developer
- **Abhilash Choudhary** (Capstone Student)
