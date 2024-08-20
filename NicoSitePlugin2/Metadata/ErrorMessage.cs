using Newtonsoft.Json;
using NicoSitePlugin.Metadata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoSitePlugin.Metadata
{
    public class ErrorMessage : IMetaMessage
    {

        public ErrorMessage(string raw)
        {
            dynamic d = JsonConvert.DeserializeObject(raw);
            reason = (string)d.data.code;
            Raw = raw;
        }

        public string Raw { get; }
        public string reason { get; }
    }
}
