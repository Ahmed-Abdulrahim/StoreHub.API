# 🛒 StoreHub.API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Project Structure](#-project-structure)
- [Installation](#-installation)

## 🎯 Overview

**StoreHub.API** is a robust, enterprise-level e-commerce REST API built with ASP.NET Core 8.0 following **Clean Architecture** principles. It provides a comprehensive backend solution for modern online stores, featuring product management, shopping cart functionality, secure authentication, order processing, and integrated payment processing.

### Problem It Solves

StoreHub.API addresses the complexity of building scalable e-commerce platforms by providing:
- A well-structured, maintainable codebase following industry best practices
- Complete e-commerce functionality out of the box
- Secure user authentication and authorization
- High-performance caching mechanisms
- Integration with payment providers (Stripe)
- Production-ready architecture that separates concerns and enables easy testing

## ✨ Features

### 🔐 Authentication & Authorization
- **JWT-based authentication** with secure token management
- **ASP.NET Core Identity** integration for user management
- **Role-based authorization** (Admin, SuperAdmin)
- Email existence validation
- Secure password handling
- User profile management with shipping addresses

### 🛍️ Product Management
- Browse products with **filtering, sorting, and pagination**
- Search products by name, brand, or type
- Sort options: name (ascending/descending), price (ascending/descending)
- Get product details by ID
- Retrieve all brands and product types
- **Response caching** for improved performance

### 🛒 Shopping Cart (Basket)
- **Redis-backed shopping cart** for high performance
- Create and update customer baskets
- Retrieve basket by customer ID
- Delete basket functionality
- Support for multiple items per basket

### 📦 Order Management
- Create orders with shipping address
- Retrieve orders by ID
- Get all orders for authenticated user
- Support for multiple delivery methods
- Order item tracking with product details
- Integration with payment status

### 💳 Payment Processing
- **Stripe integration** for secure payment processing
- Create payment intents for baskets
- Payment status tracking
- Webhook support for payment confirmations

### 🚀 Advanced Features
- **AutoMapper** for efficient object mapping
- **Specification pattern** for complex queries
- **Unit of Work pattern** for transaction management
- **Repository pattern** for data access abstraction
- **Custom middleware** for error handling
- **Swagger/OpenAPI** documentation
- **Database seeding** for initial data
- **Entity Framework Core** with migrations

## 🛠️ Technologies

### Core Framework
- **ASP.NET Core 8.0** - Modern, cross-platform web framework
- **C# 12** - Latest C# language features
- **Entity Framework Core 8.0.20** - ORM for database access

### Database & Caching
- **SQL Server** - Primary relational database
- **Redis (StackExchange.Redis 2.10.1)** - High-performance caching and session storage
- **Microsoft.EntityFrameworkCore.SqlServer 8.0.22**
- **Microsoft.EntityFrameworkCore.Proxies 8.0.20**

### Authentication & Security
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.22** - User management
- **Microsoft.AspNetCore.Authentication.JwtBearer 8.0.22** - JWT authentication
- **ASP.NET Core Authorization** - Role-based access control

### Payment Integration
- **Stripe.net 50.0.0** - Payment processing

### Development & Documentation
- **Swashbuckle.AspNetCore 6.6.2** - API documentation
- **AutoMapper 13.0.1** - Object-to-object mapping
- **Microsoft.EntityFrameworkCore.Tools 8.0.22** - EF Core CLI tools

## 🏗️ Architecture

StoreHub.API follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│         (StoreHub.API)                  │
│  Controllers, Middleware, Extensions    │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│        Application Layer                │
│      (StoreHub.Application)             │
│  Services, DTOs, Mappings, Specs        │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│         Domain Layer                    │
│         (StoreHub.Core)                 │
│  Entities, Contracts, Interfaces        │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│      Infrastructure Layer               │
│     (StoreHub.Infrastructure)           │
│  Data Access, EF Context, Repositories  │
└─────────────────────────────────────────┘
```

### Design Patterns
- **Repository Pattern** - Abstraction over data access
- **Unit of Work Pattern** - Transaction management
- **Specification Pattern** - Complex query encapsulation
- **Dependency Injection** - Loose coupling and testability
- **Service Layer Pattern** - Business logic encapsulation

## 📁 Project Structure

```
StoreHub.API/
├── 📂 StoreHub.API/                    # 🎯 API Layer (Presentation)
│   ├── Controller/
│   │   ├── AuthController.cs           # Authentication endpoints
│   │   ├── ProductController.cs        # Product management
│   │   ├── BasketController.cs         # Shopping cart operations
│   │   ├── OrderController.cs          # Order processing
│   │   └── PaymentController.cs        # Payment handling
│   ├── Attributes/                     # Custom attributes (caching, etc.)
│   ├── Errors/                         # Error handling and responses
│   ├── Extension/
│   │   ├── AddAppService.cs            # Application services registration
│   │   ├── AddServiceAutoMapping.cs    # AutoMapper configuration
│   │   ├── AddServicesBuiltIn.cs       # Built-in services setup
│   │   └── AddServicesForDpendencyInjection.cs
│   ├── MiddelWare/                     # Custom middleware
│   ├── Program.cs                      # Application entry point
│   └── appsettings.json                # Configuration settings
│
├── 📂 StoreHub.Application/            # 💼 Application Layer (Business Logic)
│   ├── Dtos/
│   │   ├── AuthDto/                    # Authentication DTOs
│   │   ├── OrderDto/                   # Order DTOs
│   │   ├── CustomBasket/               # Basket DTOs
│   │   ├── ProductDto.cs
│   │   ├── ProductRequestDto.cs
│   │   ├── BrandDto.cs
│   │   └── TypeDto.cs
│   ├── Services/
│   │   ├── AuthService.cs              # Authentication business logic
│   │   ├── ProductServices.cs          # Product management logic
│   │   ├── CustomBasketService.cs      # Cart operations
│   │   ├── OrderService.cs             # Order processing logic
│   │   ├── PaymentService.cs           # Payment handling
│   │   ├── CacheService.cs             # Caching operations
│   │   └── ServiceManager.cs           # Service aggregation
│   ├── Services.Contracts/             # Service interfaces
│   ├── MappingProfile/                 # AutoMapper profiles
│   ├── Speicifications/                # Query specifications
│   │   ├── BaseSpeicification.cs
│   │   ├── OrderSpecification.cs
│   │   ├── OrderPaymentIntentIdSpecification.cs
│   │   └── ProductSpec/
│   └── Shared/                         # Shared utilities
│
├── 📂 StoreHub.Core/                   # 🏛️ Domain Layer (Entities & Contracts)
│   ├── Models/
│   │   ├── Product.cs                  # Product entity
│   │   ├── ProductBrand.cs             # Brand entity
│   │   ├── ProductType.cs              # Product type entity
│   │   ├── CustomBasket.cs             # Basket entity
│   │   ├── BaseEntity.cs               # Base entity class
│   │   ├── Identity/
│   │   │   └── AppUser.cs              # User entity
│   │   └── Orders/
│   │       ├── Order.cs                # Order entity
│   │       ├── OrderItem.cs            # Order item entity
│   │       ├── DeliveryMethod.cs       # Delivery method entity
│   │       ├── ShippingAddress.cs      # Address entity
│   │       ├── PaymentStatus.cs        # Payment status enum
│   │       └── ProductInOrderItem.cs   # Product snapshot
│   └── Contracts/
│       ├── IGenecricEntity.cs          # Generic repository interface
│       ├── IUnitOfWork.cs              # Unit of work interface
│       ├── ICustomBasketRepository.cs  # Basket repository interface
│       ├── ICashRepository.cs          # Cache repository interface
│       ├── ISpeicifactions.cs          # Specification interface
│       ├── ISpeicifications.cs
│       └── IDbInitalizer.cs            # Database initializer interface
│
├── 📂 StoreHub.Infrastructure/         # 🔧 Infrastructure Layer (Data Access)
│   ├── Data/
│   │   ├── StoreHubDbContext.cs        # Main EF DbContext
│   │   ├── Configurations/             # Entity configurations
│   │   └── Migrations/                 # EF Core migrations
│   ├── Identity/
│   │   └── StoreHubIdentityDbContext.cs # Identity DbContext
│   ├── Repository/
│   │   ├── GenericRepository.cs        # Generic repository implementation
│   │   ├── CustomBasketRepository.cs   # Redis-based basket repository
│   │   └── CashRepository.cs           # Cache repository implementation
│   ├── UnitOfWork/
│   │   └── UnitOfWork.cs               # Unit of work implementation
│   ├── Speicifications/
│   │   └── SpecificationEvaluator.cs   # Specification query builder
│   ├── Seeding/                        # Database seed data (JSON files)
│   │   ├── products.json
│   │   ├── brands.json
│   │   ├── types.json
│   │   └── delivery.json
│   └── DbInitializer.cs                # Database initialization & seeding
│
└── StoreHub.API.sln                    # Solution file
```

### Layer Responsibilities

| Layer | Responsibilities | Dependencies |
|-------|-----------------|--------------|
| **API** | HTTP endpoints, routing, middleware, validation | Application |
| **Application** | Business logic, DTOs, services, specifications | Infrastructure, Core |
| **Core** | Domain entities, business rules, interfaces | None |
| **Infrastructure** | Data access, external services, repositories | Core |

## 🚀 Installation

### Prerequisites

Before you begin, ensure you have the following installed:
- **.NET 8.0 SDK** or later - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** (Express/Developer/LocalDB) - [Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Redis** (Optional for caching) - [Download](https://redis.io/download)
- **Visual Studio 2022** or **VS Code** with C# extension

### Step-by-Step Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/StoreHub.API.git
   cd StoreHub.API
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update connection strings**
   
   Edit `StoreHub.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "conn1": "Data Source=.;Initial Catalog=StoreHub.App;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=true",
       "conn2": "Data Source=.;Initial Catalog=StoreHub.Identity;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=true",
       "Redis": "localhost"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   cd StoreHub.API
   dotnet ef database update --project ../StoreHub.Infrastructure --context StoreHubDbContext
   dotnet ef database update --project ../StoreHub.Infrastructure --context StoreHubIdentityDbContext
   ```

5. **Start Redis** (if using caching)
   ```bash
   redis-server
   ```

6. **Run the application**
   ```bash
   dotnet run --project StoreHub.API
   ```

7. **Access Swagger UI**
   
   Navigate to: `https://localhost:7182/swagger/index.html`

## ⚙️ Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "conn1": "Data Source=.;Initial Catalog=StoreHub.App;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=true",
    "conn2": "Data Source=.;Initial Catalog=StoreHub.Identity;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=true",
    "Redis": "localhost"
  },
  "JwtOptions": {
    "issuer": "localHost:1782",
    "audience": "MyAydience",
    "secretKey": "Your-Secret-Key-Here-Must-Be-Long-Enough"
  },
  "AllowedHosts": "*",
  "BaseUrl": "https://localhost:7182"
}
```

### Environment Variables

For production, consider using environment variables instead of `appsettings.json`:

```bash
export ConnectionStrings__conn1="Your-SQL-Connection-String"
export ConnectionStrings__conn2="Your-Identity-Connection-String"
export ConnectionStrings__Redis="Your-Redis-Connection-String"
export JwtOptions__secretKey="Your-Secret-Key"
```

### Database Initialization

The application automatically:
- Creates databases if they don't exist
- Applies pending migrations
- Seeds initial data (products, brands, types, delivery methods)
- Creates default admin users:
  - **SuperAdmin**: `SuperAdmin@gmail.com` / `P@ssw0rd`
  - **Admin**: `Admin@gmail.com` / `P@ssw0rd`

## 📡 API Endpoints

### Authentication (`/api/Auth`)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/Auth/login` | User login | ❌ |
| POST | `/api/Auth/register` | User registration | ❌ |
| GET | `/api/Auth/{email}` | Check if email exists | ❌ |
| GET | `/api/Auth` | Get current user | ✅ |
| GET | `/api/Auth/GetAddress` | Get user shipping address | ✅ |
| POST | `/api/Auth/updateAddress` | Update shipping address | ✅ |

### Products (`/api/Product`)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Product` | Get all products (with filters) | ✅ |
| GET | `/api/Product/{id}` | Get product by ID | ❌ |
| GET | `/api/Product/GetBrands` | Get all brands | ❌ |
| GET | `/api/Product/GetTypes` | Get all product types | ❌ |

**Query Parameters for GET /api/Product:**
- `sort` - Sorting option: `nameasc`, `namedesc`, `priceasc`, `pricedesc`
- `pageIndex` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)
- `search` - Search term
- `brandId` - Filter by brand ID
- `typeId` - Filter by type ID

### Basket (`/api/Basket`)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/Basket/{id}` | Get basket by ID | ❌ |
| POST | `/api/Basket` | Create/Update basket | ❌ |
| DELETE | `/api/Basket/{id}` | Delete basket | ❌ |

### Orders (`/api/Order`)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/Order` | Create new order | ✅ |
| GET | `/api/Order/{id}` | Get order by ID | ✅ |
| GET | `/api/Order` | Get all user orders | ✅ |
| GET | `/api/Order/Delivery` | Get delivery methods | ✅ |

### Payment (`/api/Payment`)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/Payment/{basketId}` | Create payment intent | ✅ |

### Contribute
Contributions are welcome! Please follow these guidelines:

### How to Contribute

1. **Fork the repository**
   ```bash
   git clone https://github.com/Ahmed-Abdulrahim/StoreHub.API.git
   ```

2. **Create a feature branch**
   ```bash
   git checkout -b feature/amazing-feature
   ```

3. **Make your changes**
   - Follow C# coding conventions
   - Maintain clean architecture principles
   - Add XML documentation comments
   - Write unit tests for new features

4. **Commit your changes**
   ```bash
   git commit -m "Add: Amazing new feature"
   ```

5. **Push to the branch**
   ```bash
   git push origin feature/amazing-feature
   ```

6. **Open a Pull Request**

### Coding Standards

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Keep methods small and focused
- Add XML documentation for public APIs
- Maintain test coverage above 70%


## 📞 Contact & Support

- **Author**: Ahmed Abdulrahim
- **GitHub**: [@Ahmed-Abdulrahim](https://github.com/Ahmed-Abdulrahim)
- **Email**: ahmedabdulrahim92001@gmail.com

### 🌟 Show Your Support

If you find this project helpful, please consider:
- ⭐ Starring the repository
- 🐛 Reporting bugs and issues
- 💡 Suggesting new features
- 🤝 Contributing to the codebase

---

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Stripe API Documentation](https://stripe.com/docs/api)
- [Redis Documentation](https://redis.io/documentation)
