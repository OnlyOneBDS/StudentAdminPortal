using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.DTOs;

namespace StudentAdminPortal.Svc.Helpers;

public class UpdateStudentDtoAfterMap : IMappingAction<UpdateStudentDto, Student>
{
  public void Process(UpdateStudentDto source, Student destination, ResolutionContext context)
  {
    destination.Address = new Address
    {
      PhysicalAddress = source.PhysicalAddress,
      MailingAddress = source.MailingAddress
    };
  }
}
