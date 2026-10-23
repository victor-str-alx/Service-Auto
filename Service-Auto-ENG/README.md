# Service Auto — English Edition

## Overview

Service Auto is an auto repair shop management project built as university coursework. The repository includes a static HTML, CSS, and JavaScript frontend and an ASP.NET Core MVC application using Entity Framework Core and SQLite.

This directory documents the English edition of the project. The application's code remains in the existing `frontend/` and `ServiceAutoApp/` folders; this README does not imply that a second, separate English codebase exists.

## Technology Stack

| Area | Technologies |
|---|---|
| Backend | .NET 8, ASP.NET Core MVC, Razor views |
| Data access | Entity Framework Core 8 |
| Database | SQLite |
| Frontend | HTML, CSS, JavaScript |

## Main Features

- Multi-page frontend for services, appointments, pricing, contact, and account-related screens.
- ASP.NET Core MVC application with a Controller → Service → Repository structure.
- Data management workflows for service types, customers, vehicles, employees, appointments, and appointment-service relationships.
- Relational database model with Entity Framework Core migrations.

## Repository Structure

The main implementation is maintained at the repository root:

- `frontend/` — static HTML/CSS/JavaScript website.
- `ServiceAutoApp/` — ASP.NET Core MVC backend, models, controllers, services, repositories, views, and database migrations.
- `docs/` — project specifications and database documentation.
- `scripts/` — helper scripts.

## Run the Application

### Backend

Prerequisites: install the .NET 8 SDK.

From the repository root:

```bash
cd ServiceAutoApp
dotnet run
```

The exact local URL is printed by the .NET application when it starts. Configuration and database settings are defined in the project files.

### Static Frontend

The static frontend can be served with Python 3:

```bash
cd frontend
python3 -m http.server 8000
```

Then open http://localhost:8000.

Some frontend flows, such as appointment submissions, may rely on the backend API being available. A static frontend served on its own does not provide the backend functionality.

## Data Management Perspective

This project demonstrates how a business application organises operational records and relationships. Its data model provides context for understanding relational data, CRUD workflows, and the structure behind SQL queries. The repository is primarily an application-development project rather than a dedicated data analytics project.

## Notes

- This documentation describes the current repository layout; it does not claim that a separate translated copy of every page or source file is included.
- Consult the main README and project configuration for additional implementation details.
