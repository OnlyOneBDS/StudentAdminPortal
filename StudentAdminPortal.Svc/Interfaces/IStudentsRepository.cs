using StudentAdminPortal.Svc.Data;

namespace StudentAdminPortal.Svc.Interfaces;

public interface IStudentsRepository
{
  Task<List<Student>> GetStudentsAsync();
}
