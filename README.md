Doctor License Management System
A full-stack Doctor License Management module built for a Medical SaaS platform technical assessment.
Tech Stack
Backend
    • ASP.NET Core 8 Web API
    • Clean Architecture
    • CQRS with MediatR
    • Entity Framework Core
    • SQL Server
    • Dapper
    • FluentValidation
    • AutoMapper
    • Swagger
Frontend
    • Next.js (App Router)
    • TypeScript
    • Tailwind CSS

Features
Backend Features
    • CRUD operations for doctors
    • CQRS pattern using MediatR
    • Repository Pattern
    • Global Exception Middleware
    • FluentValidation request validation
    • AutoMapper object mapping
    • Soft delete support
    • Pagination support
    • SQL Server stored procedure integration
    • Search & filter support
Frontend Features
    • Doctor listing table
    • Search by name/license
    • Filter by status
    • Pagination
    • Add doctor modal
    • Edit doctor modal
    • Delete doctor
    • Status badges
    • Expired doctor highlighting
    • Loading states
    • Responsive modern UI

Business Rules
    • Duplicate license numbers are not allowed
    • Expired licenses are automatically marked as Expired
    • Soft delete is implemented
    • Search supports:
        ◦ Doctor name
        ◦ License number
    • Filter supports:
        ◦ Active
        ◦ Expired
        ◦ Suspended

Architecture
Backend Architecture
The backend follows Clean Architecture principles:
DoctorLicenseManagement
│
├── API
├── Application
├── Domain
└── Infrastructure
Design Patterns Used
    • CQRS
    • Repository Pattern
    • Dependency Injection
    • Middleware Pattern

Stored Procedure
Doctor listing uses SQL Server stored procedure with:
    • Search
    • Status filter
    • Pagination
    • Expiry status logic
Stored Procedure:
sp_GetDoctors

Setup Instructions
Backend Setup
1. Navigate to backend
cd backend
2. Update connection string
Update:
DoctorLicenseManagement.API/appsettings.json
3. Apply database migrations
dotnet ef database update
4. Run API
dotnet run --project DoctorLicenseManagement.API
Swagger:
https://localhost:xxxx/swagger

Frontend Setup
1. Navigate to frontend
cd frontend
2. Install dependencies
npm install
3. Configure environment
Create:
.env.local
Add:
NEXT_PUBLIC_API_BASE_URL=https://localhost:xxxx/api
4. Run frontend
npm run dev
Frontend:
http://localhost:3000

API Endpoints
Method	Endpoint	Description
GET	/api/doctors	Get doctors
GET	/api/doctors/{id}	Get doctor by id
POST	/api/doctors	Create doctor
PUT	/api/doctors/{id}	Update doctor
PATCH	/api/doctors/{id}/status	Update status
DELETE	/api/doctors/{id}	Soft delete doctor


Screenshots

Swagger


Doctor Listing

Add/Edit Modal










Pagination & Filters











Technical Decisions
    • Used CQRS + MediatR for separation of concerns
    • Used Dapper for stored procedure execution
    • Used EF Core for transactional operations
    • Used AutoMapper for clean object mapping
    • Used FluentValidation for request validation
    • Used Tailwind CSS for fast modern UI development

Author
Hafiz Muhammad Hamza Nawaz Senior .NET Backend Engineer
