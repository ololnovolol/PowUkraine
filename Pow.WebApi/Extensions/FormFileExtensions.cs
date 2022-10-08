using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pow.WebApi.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
