using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Svc.Data;
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
}