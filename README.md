# Service Auto

## Overview

Service Auto is an auto repair shop management project with a static HTML/CSS/JavaScript frontend and an ASP.NET Core MVC backend using Entity Framework Core and SQLite. The project was developed for university coursework in web application design.

## Technology Stack

| Area | Technologies |
|---|---|
| Backend | .NET 8, ASP.NET Core MVC, Razor views |
| Data layer | Entity Framework Core 8, SQLite, code-first migrations |
| Frontend prototype | HTML, CSS, JavaScript |

## Main Components

- **Static website:** multi-page interface for services, appointments, pricing, contact, and authentication placeholders.
- **MVC application:** server-rendered pages with a Controller → Service → Repository structure.
- **CRUD and administration:** management workflows for service types, customers, vehicles, employees, appointments, and appointment-service relationships.
- **Relational data model:** tables for customers, vehicles, employees, service types, appointments, and appointment-service line items.
- **Database migrations:** the application is configured to apply migrations on startup.

## English Edition

An English mirror of the static site, documentation, and backend is available in the [Service-Auto-ENG](./Service-Auto-ENG/README.md) directory.

## Data Management Perspective

The project demonstrates how a business application organises records and relationships around operational workflows. Understanding such structures is useful background for working with relational data, although this repository is primarily a web application rather than a dedicated data analysis project.

## Repository Structure

The repository includes the frontend, the ASP.NET Core MVC application, documentation, helper scripts, and an English edition. Refer to the folders and project files for implementation details.
