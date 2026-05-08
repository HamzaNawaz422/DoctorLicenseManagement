using AutoMapper;
using DoctorLicenseManagement.Application.Common.Interfaces;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Domain.Entities;
using MediatR;

namespace DoctorLicenseManagement.Application.Doctors.Commands;

public record CreateDoctorCommand(CreateDoctorRequest Request) : IRequest<Doctor>;

public class CreateDoctorCommandHandler
    : IRequestHandler<CreateDoctorCommand, Doctor>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<Doctor> Handle(
        CreateDoctorCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _doctorRepository
            .LicenseNumberExistsAsync(request.Request.LicenseNumber);

        if (exists)
            throw new Exception("License number already exists");

        var doctor = _mapper.Map<Doctor>(request.Request);

        await _doctorRepository.AddAsync(doctor);

        return doctor;
    }
}