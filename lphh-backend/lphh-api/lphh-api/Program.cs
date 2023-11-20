using System.Text;
using lphh_api.Context;
using lphh_api.Model;
using lphh_api.Repository.AdminRepo;
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
                //credentials: true nézzük meg itt, stackoverflow link: https://stackoverflow.com/questions/63351799/react-fetch-credentials-include-breaks-my-entire-request-and-i-get-an-error
        });

        app.MapControllers();

        AddRoles();
        AddAdmin();
        AddDoctor();
        AddPatient();
        
        /*
        using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices
               .GetRequiredService<IServiceScopeFactory>()
               .CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<HospitalApiContext>())
            {
                context.Database.Migrate();
            }
        }
        */
        app.Run();

//methods

        #region AddIdentity()

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

        #endregion

        #region AddServices()

        void AddServices()
        {
            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            builder.Services.AddEndpointsApiExplorer();
    
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();  
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            builder.Services.AddScoped<IExamRepository, ExamRepository>(); //AddScoped-ben kellene használni helyesen
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>(); //biztosabb, jobban preferáltabb 
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        }

        #endregion

        #region ConfigureSwagger()

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

        #endregion

        #region AddDbContext()

        void AddDbContext()
        {
            builder.Services.AddDbContext<HospitalApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Hospital") ?? throw new InvalidOperationException("Connection string 'Hospital' not found.")));
        }

        #endregion
        
        #region AddAuthentication()
        
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
        
        #endregion

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
            var adminRepo = scope.ServiceProvider.GetRequiredService<IAdminRepository>();
            if (adminInDb == null)
            {
                var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
                var adminCreated = await userManager.CreateAsync(admin, "admin123");

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    
                    var adminID1 =  await userManager.FindByEmailAsync("admin@admin.com");

                    var newAdmin = new Admin
                    {
                        Username = "admin",
                        Email = "admin@admin.com",
                        IdentityId = adminID1.Id,
                        PhoneNumber = "+36701111111",
                        FirstName = "Walter",
                        LastName = "White"
                    };
                    
                    await adminRepo.Add(newAdmin);
                }
            }
        }
        #endregion
        
        #region AddDoctor

        void AddDoctor()
        {
            var tDoctor = CreateDoctorIfNotExists();
            tDoctor.Wait();
        }

        async Task CreateDoctorIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var doctorRepo = scope.ServiceProvider.GetRequiredService<IDoctorRepository>();
            var doctorInDb = await userManager.FindByEmailAsync("drww@lphh.com");
            if (doctorInDb == null)
            {
                var doctor1 = new IdentityUser { UserName = "drWalterW", Email = "drww@lphh.com" };
                var doctor2 = new IdentityUser { UserName = "drJesseP", Email = "drjp@lphh.com" };
                var doctor3 = new IdentityUser { UserName = "drGus", Email = "drgf@lphh.com" };
                
                var doctorCreated1 = await userManager.CreateAsync(doctor1, "string");
                var doctorCreated2 = await userManager.CreateAsync(doctor2, "string");
                var doctorCreated3 = await userManager.CreateAsync(doctor3, "string");
                
                if (doctorCreated1.Succeeded && doctorCreated2.Succeeded && doctorCreated3.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor1, "Doctor");
                    await userManager.AddToRoleAsync(doctor2, "Doctor");
                    await userManager.AddToRoleAsync(doctor3, "Doctor");
                    
                    var doctorID1 =  await userManager.FindByEmailAsync("drww@lphh.com");
                    var doctorID2 =  await userManager.FindByEmailAsync("drjp@lphh.com");
                    var doctorID3 =  await userManager.FindByEmailAsync("drgf@lphh.com");
                    
                    var newDoctor1 = new Doctor
                    {
                        Username = "drWalterW",
                        Email = "drww@lphh.com",
                        Ward = "a",
                        IdentityId = doctorID1.Id,
                        PhoneNumber = "+36701111111",
                        FirstName = "Walter",
                        LastName = "White"
                    };
                    
                    var newDoctor2 = new Doctor
                    {
                        Username = "drJesseP",
                        Email = "drjp@lphh.com",
                        Ward = "b",
                        IdentityId = doctorID2.Id,
                        PhoneNumber = "+36701111112",
                        FirstName = "Jesse",
                        LastName = "Pinkman"
                    };
                    
                    var newDoctor3 = new Doctor
                    {
                        Username = "drGus",
                        Email = "drjp@lphh.com",
                        Ward = "c",
                        IdentityId = doctorID3.Id,
                        PhoneNumber = "+36701111113",
                        FirstName = "Gustavo",
                        LastName = "Fring"
                    };
            
                    await doctorRepo.Add(newDoctor1);
                    await doctorRepo.Add(newDoctor2);
                    await doctorRepo.Add(newDoctor3);
                }
                
            }
        }

        #endregion

        #region AddPatient

        void AddPatient()
        {
            var tPatient = CreatePatientIfNotExists();
            tPatient.Wait();
        }

        async Task CreatePatientIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var patientRepo = scope.ServiceProvider.GetRequiredService<IPatientRepository>();
            var patientInDb = await userManager.FindByEmailAsync("intis@gmail.com");
            if (patientInDb == null)
            {
                var patient1 = new IdentityUser { UserName = "Inti", Email = "intis@gmail.com" };
                var patient2 = new IdentityUser { UserName = "Egidio", Email = "egidiok@gmail.com" };
                var patient3 = new IdentityUser { UserName = "Lage", Email = "lagen@gmail.com" };
                
                var patientCreated1 = await userManager.CreateAsync(patient1, "string");
                var patientCreated2 = await userManager.CreateAsync(patient2, "string");
                var patientCreated3 = await userManager.CreateAsync(patient3, "string");
                
                if (patientCreated1.Succeeded && patientCreated2.Succeeded &&patientCreated3.Succeeded)
                {
                    await userManager.AddToRoleAsync(patient1, "Patient");
                    await userManager.AddToRoleAsync(patient2, "Patient");
                    await userManager.AddToRoleAsync(patient3, "Patient");
                    
                    var patientID1 =  await userManager.FindByEmailAsync("intis@gmail.com");
                    var patientID2 =  await userManager.FindByEmailAsync("egidiok@gmail.com");
                    var patientID3 =  await userManager.FindByEmailAsync("lagen@gmail.com");
                    
                    var newPatient1 = new Patient
                    {
                        Username = "Inti",
                        Email = "intis@gmail.com",
                        MedicalNumber = "111-111-111",
                        IdentityId = patientID1.Id,
                        PhoneNumber = "+36302111111",
                        FirstName = "Inti",
                        LastName = "Sílvia"
                    };
                    
                    var newPatient2 = new Patient
                    {
                        Username = "Egidio",
                        Email = "egidiok@gmail.com",
                        MedicalNumber = "111-111-112",
                        IdentityId = patientID2.Id,
                        PhoneNumber = "+36302111112",
                        FirstName = "Egidio",
                        LastName = "Kim"
                    };
                    
                    var newPatient3 = new Patient
                    {
                        Username = "Lage",
                        Email = "lagen@gmail.com",
                        MedicalNumber = "111-111-113",
                        IdentityId = patientID3.Id,
                        PhoneNumber = "+36302111113",
                        FirstName = "Lage",
                        LastName = "Ndidi"
                    };
            
                    await patientRepo.Add(newPatient1);
                    await patientRepo.Add(newPatient2);
                    await patientRepo.Add(newPatient3);
                }
                
            }
        }

        #endregion
        
    }
}

