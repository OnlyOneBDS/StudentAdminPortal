using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using StudentAdminPortal.Svc.DTOs;
using StudentAdminPortal.Svc.Interfaces;

namespace StudentAdminPortal.Svc.Validators;

public class AddStudentValidator : AbstractValidator<AddStudentDto>
{
  public AddStudentValidator(IStudentsRepository studentsRepository)
  {
    RuleFor(s => s.FirstName).NotEmpty();
    RuleFor(s => s.LastName).NotEmpty();
    RuleFor(s => s.DateOfBirth).NotEmpty();
    RuleFor(s => s.Email).NotEmpty().EmailAddress();
    RuleFor(s => s.Mobile).GreaterThanOrEqualTo(9999999).LessThanOrEqualTo(9999999999);
    RuleFor(s => s.GenderId).NotEmpty().Must(id =>
    {
      var gender = studentsRepository.GetGendersAsync().Result.ToList().FirstOrDefault(s => s.Id == id);

      if (gender != null)
      {
        return true;
      }

      return false;
    }).WithMessage("Please select a valid gender");
    RuleFor(s => s.PhysicalAddress).NotEmpty();
    RuleFor(s => s.MailingAddress).NotEmpty();
  }
}
