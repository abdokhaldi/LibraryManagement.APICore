# 📚 Library Management System - Core API

### 🌟 Overview
A modernized, robust Backend API built with **ASP.NET 8**. This project represents a comprehensive migration of a legacy Library Management system into a high-performance, scalable, and secure RESTful architecture, adhering to the highest industry standards.

---

### 🚀 Key Features & Security
* **Dual-Token Authentication:** Implementation of **JWT Access Tokens** and **Refresh Tokens** for secure, long-lived user sessions.
* **Token Revocation (Logout):** Advanced session management allowing for manual revocation of tokens to ensure immediate access termination.
* **Secure Hashing:** Using **BCrypt** for industry-standard password hashing and protection.
* **Global Exception Handling:** A centralized Middleware that intercepts unhandled exceptions to return consistent, secure JSON responses while preventing sensitive data leakage.
* **Automated Mapping:** Orchestrated by **AutoMapper** using entity-specific profiles for clean, decoupled, and maintainable code.
* **Standardized Responses:** All API operations utilize a generic `OperationResult<T>` wrapper for uniform communication with the frontend.

---

### 🏗️ Architecture & Design Patterns
The project is built on **Clean Architecture** principles to ensure total separation of concerns and long-term maintainability:

* **Repository Pattern:** Decouples data access from business logic, facilitating easier unit testing and flexibility.
* **Unit of Work:** Coordinates transactions across multiple repositories to maintain data integrity and atomicity.
* **Dependency Injection (DI):** Leverages interface-based injection to promote loose coupling throughout the system.
* **Generic Repository:** Minimizes boilerplate code for standard CRUD operations while allowing for specialized logic.

---

### 📂 Project Structure
* **LibraryManagement.API:** Handles HTTP requests, Middleware, and Controllers.
* **LibraryManagement.BLL:** Implements core business rules, service logic, and authorization.
* **LibraryManagement.DAL:** Manages **EF Core 8** configurations, DbContext, and Migrations.
* **LibraryManagement.Domain:** Contains core Entities and domain-specific logic.
* **LibraryManagement.DTO:** Holds Data Transfer Objects and shared result contracts.

---

### 🛠️ Technology Stack
* **Framework:** ASP.NET Core 8 (Web API)
* **ORM:** Entity Framework Core 8 (SQL Server)
* **Security:** JWT, BCrypt Hashing, Refresh Token Rotation
* **Mapping:** AutoMapper (Modular Profile Configuration)
* **Documentation:** Swagger / OpenAPI

---

### ⚙️ Getting Started

1.  **Clone the Repository:**
    ```bash
    git clone [your-repository-link]
    dotnet restore
    ```

2.  **Database Setup:**
    Ensure your connection string is configured in `appsettings.json`, then run:
    ```bash
    dotnet ef database update --project LibraryManagement.DAL
    ```

3.  **Run the API:**
    ```bash
    dotnet run --project LibraryManagement.API
    ```
Note: For the first-time setup, ensure an Admin user is created via the /register endpoint (temporarily set to AllowAnonymous) to manage the system
---

### 🛡️ API Testing
Explore and test the API endpoints (Authentication, Refresh Tokens, Logout, etc.) via the Swagger UI at:
`https://localhost:[PORT]/swagger`

---

### 🔑 Contribution
Contributions and architectural feedback are welcome. Please ensure all significant changes are proposed via **Pull Requests**.
