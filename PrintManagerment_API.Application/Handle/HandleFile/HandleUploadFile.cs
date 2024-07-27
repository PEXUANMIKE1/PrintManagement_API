using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagerment_API.Application.Handle.HandleFile
{
    public class HandleUploadFile
    {
        //lưu vào project,
        //lưu ở bên thứ 3 : cloudinary

        public static async Task<string> WriteFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            string fileName = "";
            try
            {
                var extension = Path.GetExtension(file.FileName);
                fileName = $"IMG_{DateTime.Now.Ticks}{extension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
                Directory.CreateDirectory(filePath);
                var exactPath = Path.Combine(filePath, fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error writing file: {ex.Message}");
                return null;
            }
            return fileName;
        }
        public static async Task<bool> DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files", fileName);
            if (File.Exists(filePath))
            {
                try
                {
                    await Task.Run(() => File.Delete(filePath));
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

    }
}
