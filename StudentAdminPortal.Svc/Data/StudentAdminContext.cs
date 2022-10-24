using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.Svc.Data;

public class StudentAdminContext : DbContext
{
  public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options) { }

  public DbSet<Address> Addresses { get; set; }
  public DbSet<Gender> Genders { get; set; }
  public DbSet<Student> Students { get; set; }
}
