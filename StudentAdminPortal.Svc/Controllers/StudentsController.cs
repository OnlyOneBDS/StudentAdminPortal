using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.DTOs;
using StudentAdminPortal.Svc.Interfaces;

namespace StudentAdminPortal.Svc.Controllers;

public class StudentsController : BaseApiController
{
  private readonly IStudentsRepository studentsRepository;
  private readonly IMapper mapper;

  public StudentsController(IStudentsRepository studentsRepository, IMapper mapper)
  {
    this.studentsRepository = studentsRepository;
    this.mapper = mapper;
  }

  [HttpGet]
  public async Task<IActionResult> GetStudentsAsync()
  {
    var students = await studentsRepository.GetStudentsAsync();

    return Ok(mapper.Map<List<Student>>(students));
  }

  [HttpGet]
  [Route("{studentId:guid}")]
  public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
  {
    // Fetcth student details
    var student = await studentsRepository.GetStudentAsync(studentId);

    if (student == null)
    {
      return NotFound();
    }

    // Return student
    return Ok(mapper.Map<Student>(student));
  }

  [HttpPut("{studentId:guid}")]
  public async Task<IActionResult> UpdateStudentAsync(Guid studentId, [FromBody] UpdateStudentDto student)
  {
    if (await studentsRepository.Exists(studentId))
    {
      // Update student details
      var studentToUpdate = await studentsRepository.UpdateStudentAsync(studentId, mapper.Map<Student>(student));

      if (studentToUpdate != null)
      {
        return Ok(mapper.Map<StudentDto>(studentToUpdate));
      }
    }

    return NotFound();
  }

  [HttpGet("genders")]
  public async Task<IActionResult> GetGenders()
  {
    var genders = await studentsRepository.GetGendersAsync();

    if (genders == null || !genders.Any())
    {
      return NotFound();
    }

    return Ok(mapper.Map<List<Gender>>(genders));
  }
}