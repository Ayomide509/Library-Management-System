Library Management System - Web API
A backend-only ASP.NET Core Web API for managing books and authors in a library system. Built with clean architecture principles, it’s designed for scalability and easy extension into a full-stack application.
Features

CRUD Operations: Full Create, Read, Update, Delete for Books and Authors.
- JWT Authentication: Secure endpoints with JSON Web Tokens.
- Entity Framework Core: Database integration with migrations and seeding.
- Security: OWASP-compliant input validation, password hashing, and error handling.
- Unit Testing: Core functions tested with xUnit and mocked data.
- Modular Design: Structured for scalability and future enhancements.

Tech Stack

ASP.NET Core 8
Entity Framework Core
Microsoft SQL Server
JWT Authentication
xUnit (Unit Testing)

Getting Started
Prerequisites

.NET 8 SDK
Microsoft SQL Server
Git

Installation

Clone the Repository  
git clone https://github.com/your-username/library-management-api.git
cd library-management-api


Configure DatabaseUpdate the MS SQL Server connection string in appsettings.json.

Apply Migrations  
dotnet ef database update


Run the Application  
dotnet run


Test EndpointsVisit https://localhost:{port}/swagger for Swagger UI.


API Endpoints
Books

GET /books - List all books
GET /books/{id} - Get a book by ID
POST /books - Create a book
PUT /books/{id} - Update a book
DELETE /books/{id} - Delete a book

Authors

GET /authors - List all authors
GET /authors/{id} - Get an author by ID
POST /authors - Create an author
PUT /authors/{id} - Update an author
DELETE /authors/{id} - Delete an author

Authentication

All endpoints require a JWT token in the Authorization header.
Uses PasswordHelper for secure password hashing.

Running Tests
Run unit tests with:
dotnet test

Future Work

Worker Service: Developing a background service for archiving old books (in progress).
Full-Stack Expansion: Add a frontend interface.
Enhanced Features: Implement role-based authorization and logging.

License
Licensed under the MIT License.
Contributing
Contributions are welcome! Please open an issue or submit a pull request.
