using AutoMapper.Configuration;
using StudentAdminPortal.Svc.Interfaces;

namespace StudentAdminPortal.Svc.Repositories;

public class ImageRepository : IImageRepository
{
  public async Task<string> UploadImage(IFormFile imageFile, string fileName)
  {
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
    using Stream fileStream = new FileStream(filePath, FileMode.Create);

    await imageFile.CopyToAsync(fileStream);
    return GetRelativePath(fileName);
  }

  private string GetRelativePath(string fileName)
  {
    return Path.Combine(@"Resources\Images", fileName);
  }
}
