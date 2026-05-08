using AutoMapper;
using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Commands;

public record UpdateDoctorCommand(
    Guid Id,
    UpdateDoctorRequest Request
) : IRequest<bool>;

public class UpdateDoctorCommandHandler
    : IRequestHandler<UpdateDoctorCommand, bool>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public UpdateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(
        UpdateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);

        if (doctor == null)
            return false;

        var exists = await _doctorRepository
            .LicenseNumberExistsAsync(request.Request.LicenseNumber, request.Id);

        if (exists)
            throw new Exception("License number already exists");

        _mapper.Map(request.Request, doctor);

        await _doctorRepository.UpdateAsync(doctor);

        return true;
    }
}