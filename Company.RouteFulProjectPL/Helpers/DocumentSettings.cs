using NuGet.Protocol.Core.Types;
using System.Net;

namespace Company.RouteFulProject.PL.Helpers
{
    public static class DocumentSettings
    {
        //IFormFile file is the file itself
        //To make any operation on file we need file path
        //File path consists of:
         //1.Folder Location
         //2.Filename

        //GetCurrentDirectory() :it gets path of project itself DYNAMICALLY (PL)
        public static string Upload(IFormFile file,string folderName)
        {
            //1.Get Folder Location

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/files/", folderName);

            //2.Get file Name and make it unique

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            //3.Total file path
            var FilePath = Path.Combine(FolderPath, fileName);

            using var FileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;
        }

        public static void Delete(string fileName, string folderName) {

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if (File.Exists(FilePath)) {
            
                 File.Delete(FilePath);
            }
        }
    
    }
}
