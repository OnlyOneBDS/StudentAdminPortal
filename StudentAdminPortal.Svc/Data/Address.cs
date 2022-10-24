namespace StudentAdminPortal.Svc.Data;

public class Address
{
  public Guid Id { get; set; }
  public string PhysicalAddress { get; set; }
  public string MailingAddress { get; set; }

  // Navigation Property
  public Guid StudentId { get; set; }
}