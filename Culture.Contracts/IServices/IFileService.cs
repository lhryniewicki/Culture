using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface IFileService
    {
        Task<string> UploadImage(IFormFile image);
        void RemoveImage(string avatarPath);
    }
}
