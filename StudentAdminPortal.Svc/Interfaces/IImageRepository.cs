namespace StudentAdminPortal.Svc.Interfaces;

public interface IImageRepository
{
  Task<string> UploadImage(IFormFile imageFile, string fileName);
}
