﻿using Dwango.Nicolive.Chat.Service.Edge;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoSitePlugin2.Client
{
    public class SegmentServerClient
    {
        private string _uri;
        private StreamReceiver? _streamReceiver;
        public bool isDisconnect { get; set; } = false;
        private BinaryStream? stream;
        private Func<ChunkedMessage, bool, Task>? _processData;
        private bool _isInitialCommentsReceiving = false;
        private Func<Task> _onNetworkError;

        // コンストラクタ
        public SegmentServerClient(string uri, Func<ChunkedMessage, bool, Task> processData, Func<Task> onNetworkError, bool isInitialCommentsReceiving)
        {
            _uri = uri;
            var headers = new Dictionary<string, string>();
            _streamReceiver = new StreamReceiver(ProcessRawData, headers);
            stream = new BinaryStream();
            _processData = processData;
            _isInitialCommentsReceiving = isInitialCommentsReceiving;
            _onNetworkError = onNetworkError;

        }

        public async Task doConnect()
        {
           await _streamReceiver.ReceiveAsync(_uri);
            if (_streamReceiver.UnexpectedDisconnect)
            {
                await _onNetworkError();
            }
            isDisconnect = true;
           //頻繁にオブジェクトの生成と破壊を繰り返す処理なので、GCしやすいように子クラスの参照を消す
           stream = null;
           _processData = null;
           _streamReceiver = null;
           await Task.CompletedTask;
        }

        public bool disconnect()
        {
            _streamReceiver?.StopReceiving();
            return true;
        }

        public async Task ProcessRawData(byte[] data)
        {
            if (_streamReceiver == null)
            {
                return;
            }
            // データを受信するたびに処理
            Console.WriteLine($"Received {data.Length} bytes.");

            stream.AddBuffer(data);

            foreach (var item in stream.Read())
            {
               var entry = ChunkedMessage.Parser.ParseFrom(item);
               await _processData(entry, _isInitialCommentsReceiving);
            }

            stream.CheckClearBuffer();
            await Task.CompletedTask;
        }
    }


}
