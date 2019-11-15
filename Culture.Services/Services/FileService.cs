using Culture.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
namespace Culture.Services.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImage(IFormFile image)
        {
            if (image == null) return null;

            if (image.Length > 0)
            {
                Guid guid = Guid.NewGuid();
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images",guid+image.FileName);
                
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }

                return $"\\images\\{guid+image.FileName}";
            }
            throw new Exception("File length has to be more than 0");
        }

        public void RemoveImage(string avatarPath)
        {
            if (File.Exists(@"wwwroot"+avatarPath))
            {
                File.Delete(@"wwwroot"+avatarPath);
            }
        }
    }
}
