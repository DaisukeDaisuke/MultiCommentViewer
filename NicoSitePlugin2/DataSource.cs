using SitePluginCommon;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
namespace NicoSitePlugin
{
    public class DataSource : ServerBase, IDataSource
    {
        private readonly string _userAgent;

        public async Task<string> GetAsync(string url, CookieContainer cc)
        {
            var result = await GetInternalAsync(new HttpOptions
            {
                Url = url,
                Cc = cc,
                UserAgent = _userAgent,
            }, false);
            var str = await result.Content.ReadAsStringAsync();
            return str;
        }

        public Task<string> GetAsync(string url)
        {
            return GetAsync(url, null);
        }
        public DataSource(string userAgent)
        {
            _userAgent = userAgent;
        }


        public async Task<string> PutAsync(string url, CookieContainer cc, string jsonString, Dictionary<string, string> headers)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var result = await PutInternalAsync(new HttpOptions
            {
                Url = url,
                Cc = cc,
                UserAgent = _userAgent,
                Headers = headers,
            }, content);
            var str = await result.Content.ReadAsStringAsync();
            return str;
        }

    }
}
