using lph_api.Model;
using lph_api.Repository.DoctorRepo;
using lph_api.Repository.EventRepo;
using lph_api.Repository.ExamRepo;
using lph_api.Repository.PatientRepo;
using lph_api.Repository.PrescriptionRepo;
using lph_api.Repository.ProductRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddSingleton<IExamRepository, ExamRepository>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers();

app.Run();