# Community Library Management API

## Project Description

The Community Library Management API is a Web API for managing books, members, and book loans. It allows users to manage library records, borrow books, and return books.

## Setup and Run

### 1. Create the Database

Open SQL Server Management Studio (SSMS).

Run these SQL scripts in this order:

1. `database/database-design.sql`
2. `database/database-content.sql`

Run `database-design.sql` first to create the database and tables. Then run `database-content.sql` to add the sample data.

### 2. Connection String

The database connection string is configured in `appsettings.json`.

The project uses SQL Server LocalDB:

```text
Server=(localdb)\MSSQLLocalDB;Database=CommunityLibraryDb;Trusted_Connection=True;TrustServerCertificate=True;
```

If a different SQL Server instance is used, update the connection string in `appsettings.json`.

### 3. Run the Application

Open a terminal in the project folder and run:

```text
dotnet run
```

After the application starts, open Swagger using the URL shown in the terminal and add:

```text
/swagger/index.html
```

Swagger can be used to test the API endpoints.

## API Endpoints

### Books

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/books` | Get all books |
| GET | `/api/books/{id}` | Get a book by ID |
| POST | `/api/books` | Add a new book |
| PUT | `/api/books/{id}` | Update a book |
| DELETE | `/api/books/{id}` | Delete a book |

### Members

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/members` | Get all members |
| GET | `/api/members/{id}` | Get a member by ID |
| POST | `/api/members` | Add a new member |
| PUT | `/api/members/{id}` | Update a member |
| DELETE | `/api/members/{id}` | Deactivate a member |

### Loans

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/loans` | Get all loans |
| GET | `/api/loans/{id}` | Get a loan by ID |
| POST | `/api/loans` | Borrow a book |
| POST | `/api/loans/{id}/return` | Return a book |
| GET | `/api/members/{id}/loans` | Get a member's loans |

## Layer Structure and Dependency Injection

The project uses a layered structure. Controllers handle HTTP requests, services handle business rules, repositories handle database operations, and the DbContext connects to the database. DTOs are used for API data. The DbContext, repositories, and services use **Scoped** dependency injection.

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext
    ↓
Database
```

## Team & Contributions

### Nichole Pebris – Member 1

- Database and EF Core setup
- Books module and CRUD operations
- Swagger setup
- README and documentation

### Maxene Garcia – Member 2

- Members module and CRUD operations
- Loans module
- Borrow and return business rules
- Loan endpoints
