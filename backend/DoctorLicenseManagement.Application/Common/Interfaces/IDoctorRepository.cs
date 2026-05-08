using DoctorLicenseManagement.Application.Common.Models;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Domain.Entities;

namespace DoctorLicenseManagement.Application.Common.Interfaces;

public interface IDoctorRepository
{
    Task<PagedResult<DoctorDto>> GetAllAsync(
    string? search,
    int? status,
    int pageNumber,
    int pageSize);

    Task<Doctor?> GetByIdAsync(Guid id);

    Task<bool> LicenseNumberExistsAsync(string licenseNumber, Guid? excludeDoctorId = null);

    Task AddAsync(Doctor doctor);

    Task UpdateAsync(Doctor doctor);

    Task DeleteAsync(Doctor doctor);
}