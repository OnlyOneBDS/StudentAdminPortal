using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.Helpers;
using StudentAdminPortal.Svc.Interfaces;
using StudentAdminPortal.Svc.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminDb")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
