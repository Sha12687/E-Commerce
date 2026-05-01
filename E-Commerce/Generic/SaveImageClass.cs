using E_Commerce.Data2.Models;

namespace E_Commerce.Generic
{
    public class SaveImageClass
    {
        //FileStream
        //    Creates a connection between your app and a physical file on disk
        //    FileMode.Create → creates new file(or overwrites if exists)

        //    vm.ImageFile.CopyToAsync(stream)
        //    Browser Upload → IFormFile(RAM) → FileStream → wwwroot / images / file.jpg
        //using (...)
        //    Ensures the file is closed & memory released properly
        //    Prevents file locks and memory leaks

        public static async Task<string?> SaveImage(IFormFile? file)
        {
            if (file == null)
                return null;

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
