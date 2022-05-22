using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core.Utilities.FileUpload
{
    public class FtpManager : IFtpService
    {
        private readonly IConfiguration _configuration;
        public FtpManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static readonly string[] ImageTypes =
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/jpg",
            "image/bmp",
            "image/bitmap",
            "image/webp"
        };
        public static readonly string[] VideoTypes =
        {
            "video/mp4",
            "video/m3u8",
            "video/rmvb",
            "video/avi",
            "video/swf",
            "video/3gp",
            "video/mkv",
            "video/flv",
            "video/mov",
            "video/quicktime"
        };
        public static readonly string[] AudioTypes =
        {
            "audio/mp3",
            "audio/wav",
            "audio/wma",
            "audio/ogg",
            "audio/aac",
            "audio/flac",
        };
        public static readonly string[] DocumentTypes =
        {
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "document/doc",
            "document/txt",
            "document/docx",
            "document/pages",
            "document/epub",
            "document/pdf",
            "document/numbers",
            "document/csv",
            "document/xls",
            "document/xlsx",
            "document/keynote",
            "document/ppt",
            "document/pptx",
        };
        public async Task<string> UploadFile(string fileNamePreWord, IFormFile file)
        {
            var ftpConfiguration = _configuration.GetSection("FTPConfiguration").Get<FtpConfiguration>();
            if (file.Length > 0)
            {
                var guid = Guid.NewGuid().ToString().Substring(0, 4);
                var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                var dateTime = $@"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{DateTime.Now.Millisecond}";
                var fileName = $@"hesapizi_{file.ContentType.GetFileContentType().Split('/')[0]}_{fileNamePreWord}_{dateTime}_{guid}{extension}";

                byte[] fileContents;
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileContents = ms.ToArray();
                }

                if (ImageTypes.Contains(file.ContentType))
                {
                    var img = ConvertByteArrayToImage(fileContents);
                    img = RotateImage(img, out bool changed);
                    if (changed)
                    {
                        fileContents = ConvertImageToByteArray(img);
                    }
                    img.Dispose();
                }

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpConfiguration.Address + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpConfiguration.UserName, ftpConfiguration.Password);

                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                return fileName;
            }
            else
            {
                return string.Empty;
            }
        }

        public bool DeleteFile(string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (File.Exists(pathToSave))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception)
                {
                }
                return true;
            }
            return false;
        }

        public bool FileTypeControl(string fileType)
        {
            return ImageTypes.Contains(fileType) || VideoTypes.Contains(fileType) || AudioTypes.Contains(fileType) || DocumentTypes.Contains(fileType);
        }

        public bool FileSizeControl(string fileType, long fileSize)
        {
            long fileSizeKb = fileSize / 1024;
            if (ImageTypes.Contains(fileType) && fileSizeKb <= 4000)
            {
                return true;
            }

            if (VideoTypes.Contains(fileType) && fileSizeKb <= 500000)
            {
                return true;
            }

            if (AudioTypes.Contains(fileType) && fileSizeKb <= 10000)
            {
                return true;
            }

            if (DocumentTypes.Contains(fileType) && fileSizeKb <= 20000)
            {
                return true;
            }

            return false;
        }

        public bool IsVideo(string fileType)
        {
            return VideoTypes.Contains(fileType);
        }

        public Image ConvertByteArrayToImage(byte[] array)
        {
            using var ms = new MemoryStream(array);
            return Image.FromStream(ms);
        }

        public byte[] ConvertImageToByteArray(Image image)
        {
            using var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public Image RotateImage(Image originalImage, out bool changed)
        {
            changed = false;
            if (originalImage.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;

                    case 8: // rotated 90 right
                            // de-rotate:
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;

                    case 3: // bottoms up
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;

                    case 6: // rotated 90 left
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;
                }
            }
            return originalImage;
        }
    }
}
