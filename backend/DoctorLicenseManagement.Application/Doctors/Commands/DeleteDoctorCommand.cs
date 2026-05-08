using DoctorLicenseManagement.Application.Common.Interfaces;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Commands;

public record DeleteDoctorCommand(Guid Id) : IRequest<bool>;

public class DeleteDoctorCommandHandler
    : IRequestHandler<DeleteDoctorCommand, bool>
{
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<bool> Handle(
        DeleteDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);

        if (doctor == null)
            return false;

        await _doctorRepository.DeleteAsync(doctor);

        return true;
    }
}