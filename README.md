# Service Auto

Web application for an **auto repair shop**: a static HTML/CSS/JS frontend and an **ASP.NET Core MVC** backend with **Entity Framework Core** and **SQLite** (university *Web Application Design* coursework).

## Tech stack

| Area | Technologies |
|------|----------------|
| Backend | .NET 8, ASP.NET Core MVC, Razor views |
| Data | EF Core 8 (code-first), SQLite, migrations |
| Frontend (prototype) | HTML, CSS, JavaScript |

## Features

- **Static site** — multi-page UI for services, appointments, pricing, contact, and auth placeholders.
- **MVC app** — server-rendered pages with layered architecture (**Controller -> Service -> Repository**); CRUD/admin for service types, customers, vehicles, employees, appointments, and appointment-service links.
- **Relational model** — six custom tables (no default Identity `AspNetUsers` in scope): customers, vehicles, employees, service types, appointments, and appointment–service line items.
- **Migrations** — applied automatically on startup via `Database.Migrate()` in `Program.cs`.

## English edition

A full **English mirror** of the static site, docs, and backend lives in **[`Service-Auto-ENG/`](./Service-Auto-ENG/README.md)** (folder name avoids `:` for Windows compatibility). It uses a separate SQLite file and suggested ports so you can run it next to the Romanian version.

## Repository layout

```
service-auto/
├── README.md                 # This file
├── Service-Auto-ENG/         # English edition (mirror)
├── scripts/                  # Helper scripts (e.g. local .NET SDK install)
├── docs/                     # Specifications and diagrams
│   ├── README.md             # Documentation index
│   ├── SPECIFICATII-APLICATIE.md
│   └── DATABASE-DIAGRAM.md
├── frontend/                 # Static site (assignment 1, Romanian UI)
│   ├── README.md
│   ├── *.html
│   ├── css/
│   └── js/
└── ServiceAutoApp/           # ASP.NET Core MVC backend (assignment 2)
    ├── Controllers/
    ├── Models/
    ├── Data/                 # ApplicationDbContext
    ├── Migrations/
    ├── Views/
    └── wwwroot/
```

## Prerequisites

- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**  
- **Python 3** (optional) — to serve the static frontend locally  
- If you use a **project-local** .NET installation, point `PATH` and `DOTNET_ROOT` to that SDK before running `dotnet` commands. The official install script is vendored at [`scripts/dotnet-install.sh`](./scripts/dotnet-install.sh) if you need to bootstrap a local SDK.

## Run the static frontend

Serve the `frontend/` folder so asset paths (`css/`, `js/`) resolve correctly:

```bash
cd frontend
python3 -m http.server 8000
```

Open **http://localhost:8000**.

`appointments.html` now sends real booking requests to backend API (`POST http://localhost:5099/api/appointments`), so keep backend running while testing the form.

## Run the backend

From repository root:

```bash
cd ServiceAutoApp

# If `dotnet` is not found, use the project-local SDK from `service-auto/.dotnet`.
export DOTNET_ROOT="/Users/victorstrimbeanu/Documents/Facultate/PAW/service-auto/.dotnet"
export PATH="/Users/victorstrimbeanu/Documents/Facultate/PAW/service-auto/.dotnet:$PATH"

dotnet --info
dotnet run --urls http://localhost:5099
```

Run the `export` lines once per terminal session.
If you changed backend code (controllers/services), stop and start `dotnet run` again so new endpoints are loaded.

| URL | Description |
|-----|-------------|
| http://localhost:5099 | Home |
| http://localhost:5099/ServiceTypes | CRUD servicii |
| http://localhost:5099/Customers | CRUD clienti |
| http://localhost:5099/Vehicles | CRUD vehicule |
| http://localhost:5099/Employees | CRUD angajati |
| http://localhost:5099/Appointments | CRUD programari |

The SQLite database file is created/updated according to `appsettings.json` (`ConnectionStrings:DefaultConnection`). Initial schema comes from EF migrations in `Migrations/`; new environments pick up changes on first run.
Current DB file path (when running from `ServiceAutoApp/`): `ServiceAutoApp/serviceauto.db`.

### EF Core tooling (optional)

To add migrations after model changes (from `ServiceAutoApp/`):

```bash
dotnet ef migrations add <MigrationName>
```

Ensure the `dotnet-ef` tool is installed if the command is not found:  
`dotnet tool install --global dotnet-ef`

## Documentation

See **[docs/README.md](./docs/README.md)** for an index of all project documentation.

## Deploy Notes

- Backend can be deployed as a standard ASP.NET Core app (`dotnet publish -c Release`).
- For production SQLite location, set connection string via environment variable:
  - `ConnectionStrings__DefaultConnection=Data Source=/absolute/path/serviceauto.db`
- Make sure the runtime process has write permissions to the folder containing the DB file.

## Editor notes

VS Code is configured (`.vscode/settings.json`) to hide generated folders such as `bin`, `obj`, `.dotnet`, and `.tools` in the file explorer. See `.gitignore` for what is excluded from version control.
# Service-Auto
# Service-Auto
# Service-Auto
