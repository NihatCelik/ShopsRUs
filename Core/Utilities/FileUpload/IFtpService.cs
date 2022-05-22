using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Threading.Tasks;

namespace Core.Utilities.FileUpload
{
    public interface IFtpService
    {
        Task<string> UploadFile(string fileNamePreWord, IFormFile file);
        bool DeleteFile(string fileName);
        bool FileTypeControl(string fileType);
        bool FileSizeControl(string fileType, long fileSize);
        bool IsVideo(string fileType);
        Image RotateImage(Image originalImage, out bool changed);
    }
}
