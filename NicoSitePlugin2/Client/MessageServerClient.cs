using Dwango.Nicolive.Chat.Service.Edge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NicoSitePlugin2.Client
{
   
    public class MessageServerClient
    {
        private string nextStreamAt = "now";
        private Func<ChunkedEntry, Task> _processData;
        private StreamReceiver _streamReceiver;
        private string _uri;
        private bool isDisconnect = false;
        private BinaryStream stream;
        private Func<Task> _onNetworkError;
        private string? BeforeNextStreamAt = null;
        public string NextStreamAt
        {
            get => nextStreamAt;
            set => nextStreamAt = value;
        }

       

        public MessageServerClient(string uri, Func<ChunkedEntry, Task> processData, Func<Task> onNetworkError)
        {
            _uri = uri;
            _processData = processData;
            stream = new BinaryStream();
            var headers = new Dictionary<string, string>
            {
                { "header", "u=1, i" }
            };
            _streamReceiver = new StreamReceiver(ProcessRawData, headers);
            _onNetworkError = onNetworkError;

        }


        public async Task doConnect()
        {
            while (!isDisconnect&&!IsUnexpectedDisconnect())
            {
                // ストリーミングの受信を開始
                await _streamReceiver.ReceiveAsync(_uri + "?at=" + nextStreamAt);
                if(BeforeNextStreamAt == NextStreamAt)
                {
                    await _onNetworkError();
                    await Task.CompletedTask;
                    return;
                }
                BeforeNextStreamAt = NextStreamAt;
            }
            if (IsUnexpectedDisconnect())
            {
                await _onNetworkError();
            }
            await Task.CompletedTask;
            
        }

        public bool IsUnexpectedDisconnect()
        {
            return _streamReceiver.UnexpectedDisconnect;
        }

        public bool disconnect()
        {
           _streamReceiver.StopReceiving();
            isDisconnect = true;
            return true;
        }


        public async Task ProcessRawData(byte[] data)
        {

            // データを受信するたびに処理
            Console.WriteLine($"Received {data.Length} bytes.");

            stream.AddBuffer(data);

            foreach (var item in stream.Read())
            {
                var entry = ChunkedEntry.Parser.ParseFrom(item);
                await _processData(entry);

            }

            stream.CheckClearBuffer();
            await Task.CompletedTask;
        }


    }
}

   
