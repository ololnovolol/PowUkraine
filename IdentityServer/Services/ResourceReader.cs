using System.Reflection;
using System.Resources;

namespace IdentityServer.Services
{
    public static class ResourceReader
    {
        public static string GetExceptionMessage(string code)
        {
            ResourceManager rm = new ResourceManager(
                "IdentityServer.Resources.Exceptions_en",
                Assembly.GetExecutingAssembly());

            string result = rm.GetString(code);

            return result ?? string.Empty;
        }
    }
}
