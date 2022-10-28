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

  public async Task<Student> GetStudentAsync(Guid studentId)
  {
    return await context.Students.Include(nameof(Address)).Include(nameof(Gender)).FirstOrDefaultAsync(s => s.Id == studentId);
  }

  public async Task<List<Student>> GetStudentsAsync()
  {
    return await context.Students.Include(nameof(Address)).Include(nameof(Gender)).ToListAsync();
  }

  public async Task<Student> UpdateStudentAsync(Guid studentId, Student student)
  {
    var studentToUpdate = await GetStudentAsync(studentId);

    if (studentToUpdate != null)
    {
      studentToUpdate.FirstName = student.FirstName;
      studentToUpdate.LastName = student.LastName;
      studentToUpdate.DateOfBirth = student.DateOfBirth;
      studentToUpdate.Email = student.Email;
      studentToUpdate.Mobile = student.Mobile;
      studentToUpdate.FirstName = student.FirstName;
      studentToUpdate.GenderId = student.GenderId;
      studentToUpdate.Address.PhysicalAddress = student.Address.PhysicalAddress;
      studentToUpdate.Address.MailingAddress = student.Address.MailingAddress;

      await context.SaveChangesAsync();

      return studentToUpdate;
    }

    return null;
  }

  public async Task<Student> DeleteStudentAsync(Guid studentId)
  {
    var student = await GetStudentAsync(studentId);

    if (student != null)
    {
      context.Students.Remove(student);
      await context.SaveChangesAsync();

      return student;
    }

    return null;
  }

  public async Task<List<Gender>> GetGendersAsync()
  {
    return await context.Genders.ToListAsync();
  }

  public async Task<bool> Exists(Guid studentId)
  {
    return await context.Students.AnyAsync(s => s.Id == studentId);
  }
}
