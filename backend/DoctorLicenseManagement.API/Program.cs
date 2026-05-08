using DoctorLicenseManagement.API.Middleware;
using DoctorLicenseManagement.Application;
using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Common.Mappings;
using DoctorLicenseManagement.Application.Doctors.Validators;
using DoctorLicenseManagement.Infrastructure.Persistence;
using DoctorLicenseManagement.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(DoctorLicenseManagement.Application.AssemblyReference).Assembly);
});


builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddAutoMapper(typeof(DoctorMappingProfile));
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();