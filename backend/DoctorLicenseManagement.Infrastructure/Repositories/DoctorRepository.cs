using Dapper;
using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Common.Models;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Domain.Entities;
using DoctorLicenseManagement.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DoctorLicenseManagement.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public DoctorRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<PagedResult<DoctorDto>> GetAllAsync(
    string? search,
    int? status,
    int pageNumber,
    int pageSize)
    {
        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));

        var parameters = new DynamicParameters();
        parameters.Add("@Search", search);
        parameters.Add("@Status", status);
        parameters.Add("@PageNumber", pageNumber);
        parameters.Add("@PageSize", pageSize);

        using var multi = await connection.QueryMultipleAsync(
            "sp_GetDoctors",
            parameters,
            commandType: CommandType.StoredProcedure);

        var doctors = (await multi.ReadAsync<DoctorDto>()).ToList();
        var totalCount = await multi.ReadFirstAsync<int>();

        return new PagedResult<DoctorDto>
        {
            Items = doctors,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task<bool> LicenseNumberExistsAsync(
        string licenseNumber,
        Guid? excludeDoctorId = null)
    {
        return await _context.Doctors.AnyAsync(x =>
            x.LicenseNumber == licenseNumber &&
            !x.IsDeleted &&
            (!excludeDoctorId.HasValue || x.Id != excludeDoctorId.Value));
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Doctor doctor)
    {
        doctor.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}