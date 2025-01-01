using System.Text.RegularExpressions;

namespace Project1.Extensions
{
    public static class FileExtensions
    {
        public static string ConvertFileToString(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;
            string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "File");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uniqueUpload, uniqueFileName);
            file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            return uniqueFileName;
        }
        public static void DeleteFileFromFileFolder(string url, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueUpload = Path.Combine(webHostEnvironment.WebRootPath, "File");
            string filePath = Path.Combine(uniqueUpload, url);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static string TypeOfFile(string FileName)
        {
            string type = null;
            string regex = @"^.+.(jpg|png|jpeg|ppt|pptx|pps|ppsx|doc|docx|pdf)$";
            if (Regex.IsMatch(FileName.ToLower(), regex))
            {
                int indexDot = FileName.LastIndexOf(".");
                type = FileName.Substring(indexDot);
            }
            return type;
        }
    }
}
