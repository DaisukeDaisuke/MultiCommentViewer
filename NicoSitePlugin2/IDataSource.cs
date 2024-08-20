using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NicoSitePlugin
{
    public interface IDataSource
    {
        Task<string> GetAsync(string url, CookieContainer cc);
        Task<string> GetAsync(string url);
        Task<string> PutJsonAsync(string url, string jsonString, CookieContainer cc, Dictionary<string, string> headers);
    }
}
