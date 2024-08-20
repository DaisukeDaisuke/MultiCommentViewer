using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NicoSitePlugin
{
    public interface IDataSource
    {
        Task<string> GetAsync(string url, CookieContainer cc);
        Task<string> GetAsync(string url);
        Task<string> PutAsync(string url, CookieContainer cc, string jsonString, Dictionary<string, string> headers);
    }
}
