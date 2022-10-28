using AutoMapper;
using StudentAdminPortal.Svc.Data;
using StudentAdminPortal.Svc.DTOs;

namespace StudentAdminPortal.Svc.Helpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Address, AddressDto>().ReverseMap();
    CreateMap<Gender, GenderDto>().ReverseMap();
    CreateMap<Student, StudentDto>().ReverseMap();
    CreateMap<UpdateStudentDto, Student>().AfterMap<UpdateStudentDtoAfterMap>();
    CreateMap<AddStudentDto, Student>().AfterMap<AddStudentDtoAfterMap>();
  }
}
