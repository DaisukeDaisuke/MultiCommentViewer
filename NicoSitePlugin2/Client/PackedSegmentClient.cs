using Dwango.Nicolive.Chat.Service.Edge;
using NicoSitePlugin.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoSitePlugin2.Client
{
    public class PackedSegmentClient
    {
        private Func<PackedSegment, Task> _onDisconnect;
        private string _uri;
        private StreamReceiver _streamReceiver;
        private bool isDisconnect = false;
        private Func<Task> _onNetworkError;

        public PackedSegmentClient(string uri, Func<PackedSegment, Task> onDisconnect, Func<Task> onNetworkError)
        {
            _uri = uri;
            _onDisconnect = onDisconnect;
            var headers = new Dictionary<string, string>();
            _streamReceiver = new StreamReceiver(ProcessRawData, headers);

        }

        public async Task doConnect()
        {
            await _streamReceiver.ReceiveAsync(_uri);
            isDisconnect = true;
            if (_streamReceiver.UnexpectedDisconnect)
            {
                await _onNetworkError();
                await Task.CompletedTask;
                return;
            }

            var segment = PackedSegment.Parser.ParseFrom(_streamReceiver.getBuffers());
            await _onDisconnect(segment);

            await Task.CompletedTask;
        }

        public async Task ProcessRawData(byte[] data)
        {

            // データを受信するたびに処理
            Console.WriteLine($"PackedSegmentClient: Received {data.Length} bytes.");

            await Task.CompletedTask;

        }
    }
}
