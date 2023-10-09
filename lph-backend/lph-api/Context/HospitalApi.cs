using lph_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lph_api.Context;

public class HospitalApi : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    
    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Hospital;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;"); 
    }
    
    protected override void OnModelCreating(ModelBuilder builder)  //adatbázis séma konfig
    {
        //Configure the City entity - making the 'Name' unique
        builder.Entity<Patient>()
            .HasIndex(u => u.Username)  //index hozzáadás
            .IsUnique();  //egyedivé tétel
    
        builder.Entity<Patient>()
            .HasData(  //adat hozzáadás
                new Patient { Id = 1, FirstName = "John", LastName = "Doe", Username = "Test", Password = "asdf", Email = "asd@asd.hu"}
                
            );
        builder.Entity<Doctor>()
            .HasIndex(u => u.Username)  //index hozzáadás
            .IsUnique();  //egyedivé tétel
    
        builder.Entity<Doctor>()
            .HasData(  //adat hozzáadás
                new Doctor { Id = 1, FirstName = "John", LastName = "Doe", Username = "Test", Password = "asdf", Email = "asd@asd.hu", Ward = "a"}
                
            );
    }
}