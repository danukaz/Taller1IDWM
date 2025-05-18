using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CloudinaryDotNet.Actions;

namespace Taller.Src.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}