using AutoMapper;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Domain.Entities;
using DoctorLicenseManagement.Domain.Enums;

namespace DoctorLicenseManagement.Application.Common.Mappings;

public class DoctorMappingProfile : Profile
{
    public DoctorMappingProfile()
    {
        CreateMap<CreateDoctorRequest, Doctor>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid()))

            .ForMember(dest => dest.CreatedDate,
                opt => opt.MapFrom(_ => DateTime.UtcNow))

            .ForMember(dest => dest.IsDeleted,
                opt => opt.MapFrom(_ => false))

            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    src.LicenseExpiryDate.Date < DateTime.UtcNow.Date
                        ? DoctorStatus.Expired
                        : DoctorStatus.Active));

        CreateMap<UpdateDoctorRequest, Doctor>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src =>
                    src.LicenseExpiryDate.Date < DateTime.UtcNow.Date
                        ? DoctorStatus.Expired
                        : DoctorStatus.Active));
    }
}