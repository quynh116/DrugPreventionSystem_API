using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugPreventionSystem.BusinessLogic.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadVideoAsync(IFormFile file);
        Task<string> UploadDocumentAsync(IFormFile file);
    }
}
