namespace StudentAdminPortal.Svc.DTOs;

public class UpdateStudentDto
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string DateOfBirth { get; set; }
  public string Email { get; set; }
  public long Mobile { get; set; }
  public Guid GenderId { get; set; }
  public string PhysicalAddress { get; set; }
  public string MailingAddress { get; set; }
}