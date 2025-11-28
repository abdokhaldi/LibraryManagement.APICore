

 #📚 Library Management System - API Core (ASP.NET 8)

## 🌟 Overview

This repository hosts the **Backend Services** for the Library Management System. This project is a **migration** and modernization effort, moving from a legacy .NET Framework/WinForms application utilizing **ADO.NET** to the latest Microsoft technologies.

The goal is to provide robust and secure **RESTful APIs** for managing users, roles, books, and borrowing/return operations, with a strong focus on performance and security.

---

## 🛠️ Technology Stack

* **Language:** C#
* **Framework:** **ASP.NET Core 8**
* **Data Access (ORM):** **Entity Framework Core 8**
* **Database:** SQL Server
* **Authentication:** JWT Bearer Tokens
* **Documentation:** Swagger/OpenAPI

---

## 🏗️ Project Architecture

The project adheres to the **Separation of Concerns** principle, structured using a modified N-Tier model (similar to Clean/Onion Architecture) and includes the following layers:

1.  **`LibraryManagement.Api`:** The Presentation Layer, containing **Controllers** to handle HTTP requests.
2.  **`LibraryManagement.BLL`:** The **Business Logic Layer**, implementing business rules and authorization checks.
3.  **`LibraryManagement.DAL`:** The **Data Access Layer**, holding the **`DbContext`** and **Entity Framework Core** configuration for database management.
4.  **`LibraryManagement.DTO`:** Shared **Data Transfer Objects** used for inter-layer communication.
5.  **`LibraryManagement.Common`:** Shared utility services (e.g., Security and Logging).

---

## 🚀 Getting Started

### 1. Cloning the Repository

```bash
git clone <Repository Link>

### 2. Database Setup

We utilize Entity Framework Core Migrations for database initialization and updates:

```bash
# Ensure you are in the Solution directory
dotnet restore
dotnet ef database update --project LibraryManagement.DAL


### 3. Running the API

* **Via Visual Studio:** Open the solution (`.sln`) and run the `LibraryManagement.Api` project.
* **Via Command Line:**
    ```bash
    dotnet run --project LibraryManagement.Api
    ```

### 4. Accessing Swagger

After running, you can test and view the endpoint documentation via the Swagger interface, which automatically opens (usually at `https://localhost:PORT/swagger`).

---

## 🔑 Contribution

Contributions and feedback on the migration process and architecture are welcome. Please use **Pull Requests** for all proposed changes.
