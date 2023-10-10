using lph_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lph_api.Context;

public class HospitalApiContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Hospital;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;"); //env
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Configure the City entity - making the 'Name' unique
        builder.Entity<Patient>()
            .HasIndex(u => u.Username)
            .IsUnique();
    
        builder.Entity<Patient>()
            .HasData(
                new Patient {Id = 1, Username = "Smithy", Password = "Incorrect", Email = "Smith@gmail.com", PhoneNumber = "+3610123456", FirstName = "John", LastName = "Smith", CreatedAt = DateTime.Now},
                new Patient {Id = 2, Username = "Doughy", Password = "Incorrect", Email = "Doughy@gmail.com", PhoneNumber = "+3620123456", FirstName = "John", LastName = "Doe", CreatedAt = DateTime.Now},
                new Patient {Id = 3, Username = "Fizzy", Password = "Incorrect", Email = "Fizy@gmail.com", PhoneNumber = "+3630123456", FirstName = "Fizz", LastName = "Buzz", CreatedAt = DateTime.Now}
            );
        
        builder.Entity<Doctor>()
            .HasIndex(u => u.Username)
            .IsUnique();
    
        builder.Entity<Doctor>()
            .HasData(
                new Doctor {Id = 1, Username = "Smithy", Password = "Incorrect", Email = "Smith@gmail.com", PhoneNumber = "+3610123456", FirstName = "John", LastName = "Smith", Ward = "a", CreatedAt = DateTime.Now},
                new Doctor {Id = 2, Username = "Doughy", Password = "Incorrect", Email = "Doughy@gmail.com", PhoneNumber = "+3620123456", FirstName = "John", LastName = "Doe", Ward = "b", CreatedAt = DateTime.Now},
                new Doctor {Id = 3, Username = "Fizzy", Password = "Incorrect", Email = "Fizy@gmail.com", PhoneNumber = "+3630123456", FirstName = "Fizz", LastName = "Buzz", Ward = "c", CreatedAt = DateTime.Now}
            );
    }
}