# The Gizmo Gateway - Technical Exercise

This repository contains a professional RESTful API developed for **The Gizmo Gateway**, a fictional retail company. The goal of this service is to manage "Gizmo" product data efficiently and scalably.

## Architecture & Technologies

The solution was built using **.NET 8** and adheres to the principles of **Clean Architecture** to ensure maintainability, testability, and a clear separation of concerns.

### Project Structure:
* **Domain:** Contains core entities and repository interfaces (The "Heart" of the system).
* **Application:** Handles the business logic and use cases.
* **Infrastructure:** Implements external concerns, such as the **Mock Data Repository**.
* **Presentation (Web API):** Leverages **Minimal APIs** for high performance and reduced boilerplate.



## AI-Assisted Development

This project was developed using **GitHub Copilot** as a primary AI collaborator. AI tools were strategically used to:
* **Boilerplate Generation:** Quickly scaffolding the Clean Architecture folder structure and dependency injection setup.
* **Data Mocking:** Generating realistic and diverse datasets for the fictional gizmos, ensuring edge cases were covered.
* **Optimization:** Refining LINQ queries for the paginated list endpoint.
* **Automated Testing:** Assisting in the creation of comprehensive unit tests using xUnit and Moq.

## Key Features

* **Retrieve Gizmo by ID:** Optimized endpoint to fetch specific product details.
* **Paginated Gizmo List:** Efficient retrieval of products by category with support for `page` and `pageSize` parameters.
* **Mock Database:** A fully functional in-memory data store for demonstration purposes.
* **Automated Tests:** Unit tests ensuring reliability of the core logic.

## Getting Started

### Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Visual Studio 2022 or VS Code

### Installation & Execution
1. Clone the repository:
   ```bash
   git clone [https://github.com/ingcarlosmoya/GizmoGateway.git](https://github.com/ingcarlosmoya/GizmoGateway.git)