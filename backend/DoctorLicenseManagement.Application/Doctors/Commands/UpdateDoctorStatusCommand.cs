using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Domain.Enums;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Commands;

public record UpdateDoctorStatusCommand(
    Guid Id,
    UpdateDoctorStatusRequest Request
) : IRequest<bool>;

public class UpdateDoctorStatusCommandHandler
    : IRequestHandler<UpdateDoctorStatusCommand, bool>
{
    private readonly IDoctorRepository _doctorRepository;

    public UpdateDoctorStatusCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<bool> Handle(
        UpdateDoctorStatusCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);

        if (doctor == null)
            return false;

        if (!Enum.IsDefined(typeof(DoctorStatus), request.Request.Status))
            throw new Exception("Invalid status");

        doctor.Status = (DoctorStatus)request.Request.Status;

        await _doctorRepository.UpdateAsync(doctor);

        return true;
    }
}