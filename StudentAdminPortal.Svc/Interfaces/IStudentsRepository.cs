using StudentAdminPortal.Svc.Data;

namespace StudentAdminPortal.Svc.Interfaces;

public interface IStudentsRepository
{
  Task<List<Student>> GetStudentsAsync();
  Task<Student> GetStudentAsync(Guid studentId);
  Task<Student> UpdateStudentAsync(Guid studentId, Student student);

  Task<List<Gender>> GetGendersAsync();

  Task<bool> Exists(Guid studentId);
}
