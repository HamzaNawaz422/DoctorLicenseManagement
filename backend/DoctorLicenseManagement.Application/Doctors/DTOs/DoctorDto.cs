namespace DoctorLicenseManagement.Application.Doctors.Dtos;

public class DoctorDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public DateTime LicenseExpiryDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}