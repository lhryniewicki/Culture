using Culture.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
// to do unikatowosc przechowywania plikow
namespace Culture.Services.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImage(IFormFile image)
        {

            if (image.Length > 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images",image.FileName);
                
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fs);
                }

                return $"\\images\\{image.FileName}";
            }
            throw new Exception("File length has to be more than 0");
        }
    }
}
