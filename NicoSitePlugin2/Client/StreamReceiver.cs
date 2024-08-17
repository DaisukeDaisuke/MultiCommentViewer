using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
        public StreamReceiver(Func<byte[], Task> processData)
        {
            _httpClient = new HttpClient();
            _processData = processData;
            _buffer = new List<byte>();
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
                        int bytesRead;
                        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            byte[] dataChunk = new byte[bytesRead];
                            Array.Copy(buffer, dataChunk, bytesRead);
                            _buffer.AddRange(dataChunk);

                            // Process the received data chunk
                            await _processData(dataChunk);
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
                await Task.Delay(1000);
            }
            await Task.CompletedTask;
        }

        public void StopReceiving()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}