# Price Management API

A robust backend service for managing item prices, suppliers, and price history tracking, built with **.NET 10** and **MySQL**.

## 🚀 Features

- **Item Management**: Complete CRUD operations for products/items with category and status tracking.
- **Supplier Management**: Manage supplier information and contact details.
- **Price Management**: Assign and track multiple prices for items from different suppliers.
- **Price History Audit**: Automatic logging of all price changes (Create, Update, Delete) with old/new value tracking.
- **Advanced Pagination**: Optimized endpoints for data tables with search and filtering.
- **Modern API Documentation**: Integrated with **Scalar** for a sleek API reference experience.

## 🛠 Tech Stack

- **Framework**: .NET 10 Web API
- **ORM**: Entity Framework Core (EF Core)
- **Database**: MySQL 8.x
- **API Reference**: Scalar (OpenAPI 3.0)

## 🛠 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

### Configuration

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=price_management;User=root;Password=your_password"
}
```

### Restore Dependencies

After cloning the repository, the `bin/` and `obj/` folders are not included (they are git-ignored). Run the following command to restore NuGet packages and generate them:

```bash
dotnet restore
```

> This step is required before running migrations or starting the application.

### Database Setup

1. **Install EF Core Tools** (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. **Apply Migrations**:
   ```bash
   dotnet ef database update
   ```

*Note: A SQL script is also available at `Data/script_db.sql` for manual initialization if preferred.*

### Running the API

```bash
dotnet run
```

The API will be available at `http://localhost:7077` (or the port specified in `Properties/launchSettings.json`).

## 📖 API Documentation

Once the application is running, you can access the interactive API reference:

- **Scalar API Reference**: `http://localhost:5086/scalar/v1` (or your local port)

## 📂 Project Structure

- `Controllers/`: API endpoints handling HTTP requests.
- `Services/`: Business logic layer.
- `Interfaces/`: Service abstractions.
- `Entities/`: Database models.
- `DTOs/`: Data Transfer Objects for API requests/responses.
- `Data/`: DBContext and migration files.
- `Enums/`: Shared enumerations (e.g., ItemStatus).
