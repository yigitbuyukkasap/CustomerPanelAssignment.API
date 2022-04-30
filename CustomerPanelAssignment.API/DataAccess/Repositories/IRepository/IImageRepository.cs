using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepository
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
