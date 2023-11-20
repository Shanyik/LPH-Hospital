using lphh_api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lphh_api.Context;

public class HospitalApiContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Product> Products { get; set;  }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Event> Events { get; set; }
    
    public DbSet<Admin> Admins { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Hospital;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;");
    }*/
    
    public HospitalApiContext (DbContextOptions<HospitalApiContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Patient>()
            .HasIndex(p => new {p.Username, p.MedicalNumber})
            .IsUnique();

        builder.Entity<Doctor>()
            .HasIndex(d => d.Username)
            .IsUnique();
        
        builder.Entity<Admin>()
            .HasIndex(d => d.Email)
            .IsUnique();
        
        builder.Entity<Product>()
            .HasIndex(p => p.Name)
            .IsUnique();

        builder.Entity<Product>()
            .HasData(
                new Product {Id = 1, Name = "Flector Rapid 50", Packing = "20x", Subsitutable = true},
                new Product {Id = 2, Name = "Drisdol", Packing = "3x10", Subsitutable = true},
                new Product {Id = 3, Name = "Ventolin", Packing = "1x120", Subsitutable = false}
            );

        builder.Entity<Prescription>()
            .HasData(
                new Prescription {Id = 1, PatientId = 1, DoctorId = 1, ProductId = 1 ,Description = "1x2 for 10 days",CreatedAt = DateTime.Now},
                new Prescription {Id = 2, PatientId = 1, DoctorId = 2, ProductId = 3 ,Description = "1x1 for 120 days",CreatedAt = DateTime.Now},
                new Prescription {Id = 3, PatientId = 2, DoctorId = 3, ProductId = 2 ,Description = "1x1 for 30 days",CreatedAt = DateTime.Now}
            );
        
        builder.Entity<Exam>()
            .HasData(
                new Exam {Id = 1, Type = "General Exam", PatientId = 1, DoctorId = 1, Result = "resultString", CreatedAt = DateTime.Now},
                new Exam {Id = 2, Type = "General Exam", PatientId = 3, DoctorId = 2, Result = "resultString",  CreatedAt = DateTime.Now},
                new Exam {Id = 3, Type = "General Exam", PatientId = 2, DoctorId = 3, Result = "resultString",  CreatedAt = DateTime.Now}
            );
        
        builder.Entity<Event>()
            .HasData(
                new Event {Id = 1, Name = "Donate Blood", Description = "EventDescription", Start = DateTime.Now, End = DateTime.Now.AddDays(3), CreatedAt = DateTime.Now},
                new Event {Id = 2, Name = "General Exams", Description = "EventDescription", Start = DateTime.Now, End = DateTime.Now.AddDays(5), CreatedAt = DateTime.Now},
                new Event {Id = 3, Name = "Donate Blood", Description = "EventDescription", Start = DateTime.Now.AddDays(5), End = DateTime.Now.AddDays(10), CreatedAt = DateTime.Now}
            );
        
        base.OnModelCreating(builder);
    }
}