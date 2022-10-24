using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.Interfaces;

namespace StudentAdminPortal.Svc.Repositories;

public class StudentsRepository : IStudentsRepository
{
  private readonly StudentAdminContext context;

  public StudentsRepository(StudentAdminContext context)
  {
    this.context = context;
  }

  public async Task<List<Student>> GetStudentsAsync()
  {
    return await context.Students.Include(nameof(Address)).Include(nameof(Gender)).ToListAsync();
  }
}
