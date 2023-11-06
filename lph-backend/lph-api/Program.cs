using System.Text;
using lph_api.Context;
using lph_api.Model;
using lph_api.Repository.DoctorRepo;
using lph_api.Repository.EventRepo;
using lph_api.Repository.ExamRepo;
using lph_api.Repository.PatientRepo;
using lph_api.Repository.PrescriptionRepo;
using lph_api.Repository.ProductRepo;
using lph_api.Service.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers();

AddRoles();

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
    
    builder.Services.AddSingleton<IPatientRepository, PatientRepository>();  //ál
    builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
    builder.Services.AddSingleton<IPrescriptionRepository, PrescriptionRepository>();
    builder.Services.AddSingleton<IExamRepository, ExamRepository>(); //AddScoped-ben kellene használni helyesen
    builder.Services.AddSingleton<IEventRepository, EventRepository>();
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
    builder.Services.AddDbContext<HospitalApiContext>();
    
    void InitializeDb()
    {
        using var db = new HospitalApiContext();
    }

    InitializeDb();
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
