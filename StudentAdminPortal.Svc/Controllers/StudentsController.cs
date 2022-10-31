using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.DTOs;
using StudentAdminPortal.Svc.Interfaces;

namespace StudentAdminPortal.Svc.Controllers;

public class StudentsController : BaseApiController
{
  private readonly IStudentsRepository studentsRepository;
  private readonly IImageRepository imageRepository;
  private readonly IMapper mapper;

  public StudentsController(IStudentsRepository studentsRepository, IImageRepository imageRepository, IMapper mapper)
  {
    this.studentsRepository = studentsRepository;
    this.imageRepository = imageRepository;
    this.mapper = mapper;
  }

  [HttpGet]
  public async Task<IActionResult> GetStudentsAsync()
  {
    var students = await studentsRepository.GetStudentsAsync();

    return Ok(mapper.Map<List<Student>>(students));
  }

  [HttpGet("{studentId:guid}"), ActionName("GetStudentAsync")]
  public async Task<IActionResult> GetStudentAsync(Guid studentId)
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

  [HttpPost]
  public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentDto student)
  {
    var createdStudent = await studentsRepository.AddStudentAsync(mapper.Map<Student>(student));
    return CreatedAtAction(nameof(GetStudentAsync), new { studentId = createdStudent.Id }, mapper.Map<Student>(createdStudent));
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

  [HttpDelete("{studentId:guid}")]
  public async Task<IActionResult> DeleteStudentAsync(Guid studentId)
  {
    if (await studentsRepository.Exists(studentId))
    {
      // Delete the student
      var deletedStudent = await studentsRepository.DeleteStudentAsync(studentId);

      return Ok(mapper.Map<StudentDto>(deletedStudent));
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

  [HttpPost("upload-image/{studentId:guid}")]
  public async Task<IActionResult> UploadImage(Guid studentId, IFormFile imageFile)
  {
    if (await studentsRepository.Exists(studentId))
    {
      var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
      var imageUrl = await imageRepository.UploadImage(imageFile, fileName);

      if (await studentsRepository.UpdateStudentImage(studentId, imageUrl))
      {
        return Ok(imageUrl);
      }

      return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
    }

    return NotFound();
  }
}