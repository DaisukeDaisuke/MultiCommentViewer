using Newtonsoft.Json;

namespace NicoSitePlugin.Metadata
{
    internal class MessageServer : IMetaMessage
    {
       

        public MessageServer(string raw) {
            dynamic d = JsonConvert.DeserializeObject(raw);
            MessageServerUrl = (string)d.data.viewUri;
            Raw = raw;
        }

        public string Raw { get; }
        public string MessageServerUrl { get; }
    }
}
