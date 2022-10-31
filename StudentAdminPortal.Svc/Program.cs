using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.Helpers;
using StudentAdminPortal.Svc.Interfaces;
using StudentAdminPortal.Svc.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddCors((options) =>
{
  options.AddPolicy("AngularPolicy", (policy) =>
  {
    policy.WithOrigins("http://localhost:4200")
      .AllowAnyHeader()
      .WithMethods("DELETE", "GET", "POST", "PUT")
      .WithExposedHeaders("*");
  });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminDb")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Resources")),
  RequestPath = "/api/Resources"
});

app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
