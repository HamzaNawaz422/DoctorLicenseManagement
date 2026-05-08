using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Common.Models;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Queries.GetDoctors;

public record GetDoctorsQuery(
    string? Search,
    int? Status,
    int PageNumber,
    int PageSize
) : IRequest<PagedResult<DoctorDto>>;

public class GetDoctorsQueryHandler
    : IRequestHandler<GetDoctorsQuery, PagedResult<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<PagedResult<DoctorDto>> Handle(
        GetDoctorsQuery request,
        CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetAllAsync(
            request.Search,
            request.Status,
            request.PageNumber,
            request.PageSize);
    }
}