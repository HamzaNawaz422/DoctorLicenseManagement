using DoctorLicenseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorLicenseManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Doctor> Doctors => Set<Doctor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctors");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Specialization)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.LicenseNumber)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(x => x.LicenseNumber)
                .IsUnique();

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.CreatedDate)
                .IsRequired();

            entity.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        });
    }
}