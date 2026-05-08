using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Domain.Entities;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Queries;

public record GetDoctorByIdQuery(Guid Id) : IRequest<Doctor?>;

public class GetDoctorByIdQueryHandler
    : IRequestHandler<GetDoctorByIdQuery, Doctor?>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Doctor?> Handle(
        GetDoctorByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetByIdAsync(request.Id);
    }
}