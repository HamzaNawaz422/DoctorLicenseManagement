using DoctorLicenseManagement.Application.Doctors.Dtos;
using FluentValidation;

namespace DoctorLicenseManagement.Application.Doctors.Validators;

public class UpdateDoctorRequestValidator
    : AbstractValidator<UpdateDoctorRequest>
{
    public UpdateDoctorRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LicenseExpiryDate)
            .NotEmpty();
    }
}