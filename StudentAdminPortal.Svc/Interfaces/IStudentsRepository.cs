using StudentAdminPortal.Svc.Data;

namespace StudentAdminPortal.Svc.Interfaces;

public interface IStudentsRepository
{
  Task<List<Student>> GetStudentsAsync();
  Task<Student> GetStudentAsync(Guid studentId);
  Task<Student> AddStudentAsync(Student student);
  Task<Student> UpdateStudentAsync(Guid studentId, Student student);
  Task<Student> DeleteStudentAsync(Guid studentId);

  Task<List<Gender>> GetGendersAsync();

  Task<bool> UpdateStudentImage(Guid studentId, string imageUrl);

  Task<bool> Exists(Guid studentId);
}
