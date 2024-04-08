using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Helpers
{
    public static class FileManager
    {
        public static List<string> UploadFiles(List<IFormFile> files, int id, bool isEditMode = false, List<string> imagePaths = null)
        {
            List<string> uploadedFilePaths = new List<string>();

            if (isEditMode && imagePaths != null)
            {
                if (files == null || files.Count == 0)
                {
                    uploadedFilePaths.AddRange(imagePaths);
                    return uploadedFilePaths;
                }
            }

            string basePath = $"/Images/Propiedades/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                //get file extension
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileName = $"{id}-0{i}_{guid}{fileInfo.Extension}";

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                uploadedFilePaths.Add($"{basePath}/{fileName}");
            }

            if (isEditMode && imagePaths != null)
            {
                foreach (var imagePath in imagePaths)
                {
                    string[] oldImagePart = imagePath.Split("/");
                    string oldImagePath = oldImagePart[^1];
                    string completeImageOldPath = Path.Combine(path, oldImagePath);

                    if (System.IO.File.Exists(completeImageOldPath))
                    {
                        System.IO.File.Delete(completeImageOldPath);
                    }
                }
            }

            return uploadedFilePaths;
        }

        public static void DeletePropertyImages(List<string> imageUrls)
        {
            foreach (var imageUrl in imageUrls)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                {
                    Console.WriteLine($"El archivo '{path}' no existe.");
                }
            }
        }
    }
}
