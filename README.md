# Doctor License Management

A full-stack Doctor License Management module for a Medical SaaS platform.

The application allows users to manage doctor license records with search, filtering, pagination, status badges, add/edit modal forms, soft delete, and automatic expired license handling.

---

## Tech Stack

### Backend
- ASP.NET Core 8 Web API
- Clean Architecture
- CQRS with MediatR
- Entity Framework Core
- SQL Server
- Dapper
- FluentValidation
- AutoMapper
- Swagger

### Frontend
- Next.js App Router
- TypeScript
- Tailwind CSS

---

## Project Structure

```txt
DoctorLicenseManagement/
│
├── backend/
│   ├── DoctorLicenseManagement.API
│   ├── DoctorLicenseManagement.Application
│   ├── DoctorLicenseManagement.Domain
│   └── DoctorLicenseManagement.Infrastructure
│
├── frontend/
│
├── DatabaseScripts/
│   └── sp_GetDoctors.sql
│
└── README.md



Approach

The backend is built using Clean Architecture to keep responsibilities separated across API, Application, Domain, and Infrastructure layers.

CQRS with MediatR is used to separate read and write operations. Controllers remain thin and delegate business operations to command/query handlers.

Entity Framework Core is used for database operations such as create, update, get by id, and soft delete. Dapper is used specifically for doctor listing because the assignment requires listing data to be returned using a SQL stored procedure.

The stored procedure handles:

doctor listing
search by name or license number
status filtering
pagination
expiry status logic

The frontend is built using Next.js with Tailwind CSS. It includes a clean doctor table, search, status filter, pagination, add/edit modal, delete action, status badges, loading state, and expired doctor highlighting.


Features
1) Create doctor
2) Get all doctors
3) Get doctor by id
4) Update doctor
5) Update doctor status
6) Soft delete doctor
7) Prevent duplicate license numbers
8) Auto mark expired doctors based on license expiry date
9) Search by doctor name or license number
10) Filter by status
11) Pagination
12) Add/Edit modal
13) Expired doctor row highlight
