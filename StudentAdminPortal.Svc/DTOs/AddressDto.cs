namespace StudentAdminPortal.Svc.DTOs
{
  public class AddressDto
  {
    public Guid Id { get; set; }
    public string PhysicalAddress { get; set; }
    public string MailingAddress { get; set; }
    public Guid StudentId { get; set; }
  }
}