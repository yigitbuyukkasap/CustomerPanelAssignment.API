using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName) {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
