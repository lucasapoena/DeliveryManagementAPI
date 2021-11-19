using Application.Interfaces.Services;
using Application.Requests;
using System;
using System.IO;

namespace Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        public string UploadAsync(UploadRequest request)
        {
            var file = request.File;
            if (file == null || file.Length == 0) return string.Empty;

            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx") return string.Empty;

            if (file.Length > 0)
            {
                var folder = "ImportDeliveries";
                var folderName = Path.Combine("Files", folder);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                bool exists = System.IO.Directory.Exists(pathToSave);
                if (!exists)
                    System.IO.Directory.CreateDirectory(pathToSave);
                var fileName = $"D-{Guid.NewGuid()}-{file.FileName.Trim('"')}";
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                if (File.Exists(dbPath))
                {
                    dbPath = NextAvailableFilename(dbPath);
                    fullPath = NextAvailableFilename(fullPath);
                }

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }

                return dbPath;
            }
            else
            {
                return string.Empty;
            }
        }

        private static string numberPattern = " ({0})";

        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
                return path;

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            //if (tmp == pattern)
            //throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }
    }
}