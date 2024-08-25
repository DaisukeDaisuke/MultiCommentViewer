using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NicoSitePlugin2.Client
{
    public class StreamReceiver
    {
        private readonly HttpClient _httpClient;
        private CancellationTokenSource _cancellationTokenSource;
        private List<byte> _buffer;
        private Func<byte[], Task> _processData;
        public bool UnexpectedDisconnect { get; set; } = false;
        public StreamReceiver(Func<byte[], Task> processData, Dictionary<string, string> headers)
        {
            _httpClient = new HttpClient();
            _processData = processData;
            _buffer = new List<byte>();

            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
        public async Task ReceiveAsync(string url)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var buffer = new byte[8192];
                        int bytesRead ;

                        try
                        {
                            while(true)
                            {
                                
                                Task<int> result = stream.ReadAsync(buffer, 0, buffer.Length, _cancellationTokenSource.Token);

                                await Task.WhenAny(result, Task.Delay(1000 * 30, _cancellationTokenSource.Token));

                                if (result.IsCompleted)
                                {
                                    bytesRead = await result; // 非同期操作が完了したら、結果を取得
                                    if (bytesRead <= 0)
                                    {
                                        break;
                                    }
                                    byte[] dataChunk = new byte[bytesRead];
                                    Array.Copy(buffer, dataChunk, bytesRead);
                                    _buffer.AddRange(dataChunk);

                                    // Process the received data chunk
                                    await _processData(dataChunk);   
                                }
                                else
                                {
                                    stream.Close();
                                    StopReceiving();
                                    UnexpectedDisconnect = true;
                                    break;
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            // タイムアウトやキャンセルが発生した場合の処理
                            Console.WriteLine("Read operation was canceled due to a timeout or external cancellation.");
                        }
                        ////切断時の処理
                        //await _onDisconnect();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Operation was cancelled.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                UnexpectedDisconnect = true;
            }
            await Task.CompletedTask;
        }

        public Byte[] getBuffers()
        {
            return _buffer.ToArray();
        }

        public void StopReceiving()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}