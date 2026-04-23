# AGENTS.md

This file contains critical context for agents working in this repository to avoid mistakes and speed up ramp-up.

## Project Overview
- **Type**: WPF Desktop Application (.NET 8.0 Windows)
- **Framework**: .NET 8.0 with WPF
- **Database**: Microsoft.Data.Sqlite
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Architecture**: Single-project Windows desktop application

## Key Facts

### Project Structure
- Main project: BusinessAccountantService.csproj
- WPF UI files: .xaml and .xaml.cs files
- Data access layer: SQLite database
- UI pages: MainWindow.xaml, ClientsPage.xaml, InventoryPage.xaml, etc.
- Models: Models/ folder
- Managers: Managers/ folder

### Development Commands
- Build: `dotnet build`
- Run: `dotnet run`
- Test: `dotnet test` (if test projects exist)
- Clean: `dotnet clean`

### Development Flow
- Build with `dotnet build` to compile
- Run with `dotnet run` to start the WPF application
- The application uses WPF with XAML-based UI

### Framework Details
- SDK: Microsoft.NET.Sdk
- Target Framework: net8.0-windows
- Uses WPF: `<UseWPF>true</UseWPF>`
- Uses Nullable references: `<Nullable>enable</Nullable>`
- Target platform: Windows (uses Windows-specific features)

### Key Dependencies
- `Microsoft.Data.Sqlite`: For database access
- `LiveCharts.Wpf`: For charting in UI
- `QuestPDF`: For PDF generation

### Important Notes
- The application uses Windows-specific features (WPF)
- UI is built with XAML and C#
- Database is SQLite with Microsoft.Data.Sqlite
- The project has multiple UI pages (MainWindow, ClientsPage, InventoryPage, etc.)
- WPF uses code-behind approach (XAML.cs files)
- Application is designed for Windows desktop environment

## Special Considerations
- This is a Windows desktop application, not web or mobile
- All WPF controls are Windows-specific
- Database is SQLite, not enterprise databases
- Application uses Windows Forms UI patterns