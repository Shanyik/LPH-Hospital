using System.Text;
using lphh_api.Context;
using lphh_api.Model;
using lphh_api.Repository.DoctorRepo;
using lphh_api.Repository.EventRepo;
using lphh_api.Repository.ExamRepo;
using lphh_api.Repository.PatientRepo;
using lphh_api.Repository.PrescriptionRepo;
using lphh_api.Repository.ProductRepo;
using lphh_api.Service.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
       
        AddServices();
        ConfigureSwagger();
        AddDbContext();
        AddAuthentication();
        AddIdentity();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var db = serviceScope.ServiceProvider.GetRequiredService<HospitalApiContext>().Database;

            logger.LogInformation("Migrating database...");

            while (!db.CanConnect())
            {
                logger.LogInformation("Database not ready yet; waiting...");
                Thread.Sleep(1000);
            }

            try
            {
                serviceScope.ServiceProvider.GetRequiredService<HospitalApiContext>().Database.Migrate();
                logger.LogInformation("Database migrated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        app.MapControllers();

        AddRoles();
        AddAdmin();
        AddDoctor();
        
        using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices
               .GetRequiredService<IServiceScopeFactory>()
               .CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<HospitalApiContext>())
            {
                context.Database.Migrate();
            }
        }

        app.Run();

//methods

        void AddIdentity()
        {
            builder.Services
                .AddIdentityCore<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>() //Enable Identity roles 
                .AddEntityFrameworkStores<HospitalApiContext>();
        }

        void AddServices()
        {
            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
    
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();  
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            builder.Services.AddScoped<IExamRepository, ExamRepository>(); //AddScoped-ben kellene használni helyesen
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>(); //biztosabb, jobban preferáltabb 
            builder.Services.AddScoped<ITokenService, TokenService>();
        }

        void ConfigureSwagger()
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "LPHH API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        void AddDbContext()
        {
            builder.Services.AddDbContext<HospitalApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Hospital") ?? throw new InvalidOperationException("Connection string 'Hospital' not found.")));
        }

        void AddAuthentication()
        {
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "apiWithAuthBackend",
                        ValidAudience = "apiWithAuthBackend",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("!SomethingSecret!")
                        ),
                    };
                });
        }
        
        

        #region AddRole

        void AddRoles()
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var tAdmin = CreateAdminRole(roleManager);
            tAdmin.Wait();

            var tDoctor = CreateDoctorRole(roleManager);
            tDoctor.Wait();
    
            var tPatient = CreatePatientRole(roleManager);
            tPatient.Wait();
        }

        async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin")); 
        }

        async Task CreateDoctorRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Doctor")); 
        }
        async Task CreatePatientRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Patient")); 
        }

        #endregion
        
        #region admin

        void AddAdmin()
        {
            var tAdmin = CreateAdminIfNotExists();
            tAdmin.Wait();
        }

        async Task CreateAdminIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminInDb == null)
            {
                var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
                var adminCreated = await userManager.CreateAsync(admin, "admin123");

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
        #endregion
        
        #region AddDoctor

        void AddDoctor()
        {
            var tAdmin = CreateDoctorIfNotExists();
            tAdmin.Wait();
        }

        async Task CreateDoctorIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var doctorRepo = scope.ServiceProvider.GetRequiredService<IDoctorRepository>();
            var adminInDb = await userManager.FindByEmailAsync("doctor@test.com");
            if (adminInDb == null)
            {
                var doctor = new IdentityUser { UserName = "doctor", Email = "doctor@test.com" };
                var doctorCreated = await userManager.CreateAsync(doctor, "string");
                
                if (doctorCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor, "Doctor");
                    var doctorID =  await userManager.FindByEmailAsync("doctor@test.com");
                    var newDoctor = new Doctor
                    {
                        Username = "doctor",
                        Email = "doctor@test.com",
                        Ward = "a",
                        IdentityId = doctorID.Id,
                        PhoneNumber = "+11111111",
                        FirstName = "firstName",
                        LastName = "lastName"
                    };
            
                    await doctorRepo.Add(newDoctor);
                }
                
            }
        }

        #endregion
        
    }
}

