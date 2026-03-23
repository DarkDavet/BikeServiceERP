# 🚲 BikeLab Service Manager
**A high-performance Desktop ERP solution for bicycle workshops.**
<img width="800" height="600" alt="image" src="https://github.com/user-attachments/assets/3781c69f-d736-4f98-b6cc-c3bf4318a3cb" />
## Overview
BikeLab Service Manager is a specialized software designed to automate daily workshop operations. It simplifies customer relationship management (CRM), inventory tracking, and financial reporting in a single desktop environment.

## Key Features
*   **Order Management:** Track repair cycles from intake to completion with status monitoring (Active/Ready/Archived).
*   **Inventory Control:** Category-based stock management with automated deduction and "zero-stock" visual alerts.
*   **Financial Analytics:** Real-time dashboard for Revenue, Expenses, and Net Profit calculation with decimal precision.
*   **Automated Document Generation:** Instant creation of branded PDF Service Acts and Invoices using a custom reporting engine.
*   **Advanced Search:** High-speed data filtering and search using `ICollectionView` for seamless UX.

## Technical Stack
*   **Language:** C# (.NET 8)
*   **Framework:** WPF (XAML)
*   **Database:** SQLite (Microsoft.Data.Sqlite)
*   **Reporting:** QuestPDF, LiveCharts
*   **Architecture:** Data-driven logic with custom ControlTemplates and Triggers.

## Setup & Running
1.  Clone the repository.
2.  Open the solution in **Visual Studio**.
3.  Build and Run. The SQLite database (`bike_service.db`) will be initialized automatically on the first launch.

## Deployment
The application is optimized for **Self-contained** deployment, ensuring it runs on any Windows machine (Win 7/8/10/11) without pre-installed .NET runtimes.
