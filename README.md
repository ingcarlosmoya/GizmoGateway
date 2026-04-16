# The Gizmo Gateway - Technical Exercise

This repository contains a professional RESTful API developed for **The Gizmo Gateway**, a fictional retail company. The goal of this service is to manage "Gizmo" product data efficiently and scalably.

## Architecture & Technologies

The solution was built using **.NET 10** and adheres to the principles of **Clean Architecture** to ensure maintainability, testability, and a clear separation of concerns. By targeting the latest framework, the project leverages **C# 14** features for cleaner, more expressive code.

### Project Structure:
* **Domain:** Contains core entities (using modern C# records) and repository interfaces.
* **Application:** Handles the business logic and service orchestration.
* **Infrastructure:** Implements external concerns, specifically the **In-Memory Mock Repository**.
* **Presentation (Web API):** Leverages **Minimal APIs** and **ASP.NET Core 10** for high performance and reduced boilerplate.

## AI-Assisted Development

This project was developed using **GitHub Copilot** as a primary AI collaborator. AI tools were strategically used to:
* **Boilerplate Generation:** Scaffolding the Clean Architecture layers and modern Dependency Injection (DI) setup.
* **Data Mocking:** Generating realistic, relational datasets (Gizmos and Manufacturers) for demonstration.
* **Logic Refinement:** Crafting efficient paginated queries and ensuring proper asynchronous task handling.
* **Documentation & Setup:** Assisting with environment configuration, including `launchSettings.json` and versioning resolution.

## Key Features

* **Rich Entity Modeling:** Gizmo products now include nested **Manufacturer** entities, reflecting a real-world relational structure.
* **Financial Precision:** MSRP pricing is handled using the `decimal` type to ensure mathematical accuracy.
* **Retrieve Gizmo by ID:** Optimized endpoint to fetch specific product and manufacturer details.
* **Paginated Gizmo List:** Efficient retrieval by category with support for `page` and `pageSize` parameters.
* **Interactive Documentation:** Fully configured **Swagger/OpenAPI** UI for easy endpoint testing in development mode.

## Getting Started

### Prerequisites
* **[.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)**
* **Visual Studio 2022** (Version 17.12+) or **VS Code** with C# Dev Kit

### Installation & Execution
1.  **Clone the repository:**
    ```bash
    git clone https://github.com/ingcarlosmoya/GizmoGateway.git
    cd GizmoGateway
    ```

2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3.  **Run the Application:**
    To bypass potential local execution policy blocks, it is recommended to run the project using the .NET CLI:
    ```bash
    dotnet run --project ProjectName.Api
    ```
    *Alternatively, if your system blocks the generated .exe, run the DLL directly from the output folder:*
    ```bash
    dotnet bin/Debug/net10.0/ProjectName.Api.dll
    ```

4.  **Access Swagger:**
    Once running, navigate to `http://localhost:5000/swagger` to explore the API.