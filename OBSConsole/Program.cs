using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OBSConsole
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            List<string> urls = new List<string>();
            urls.Add(OBSServerUrl);
            urls.Add(SKUServerUrl);
            foreach (var item in urls)
            {
                await RunWebSockets(item);
            }
            
            Console.WriteLine("Hello Streamer!");
            return 0;
            
        }
        private const string OBSServerUrl = "ws://192.168.1.132:4444";
        private const string SKUServerUrl = "ws://localhost:5001/hubs/StreamKontrolUnitHub";
        private static async Task RunWebSockets(string url)
        {
            var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri(url), CancellationToken.None);
            Console.WriteLine("Connected");
            var sending = Task.Run(async () =>
            {
                string line;
                while ((line = Console.ReadLine()) != null)
                {
                    var bytes = Encoding.UTF8.GetBytes(line);
                    await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);
                }
                await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });
            var receiving = Receiving(ws);
            await Task.WhenAll(sending, receiving);

        }

        private static async Task Receiving(ClientWebSocket ws)
        {
            var buffer = new byte[2048];

            while (true)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                }
                else if (result.MessageType== WebSocketMessageType.Binary)
                {

                }
                else if (result.MessageType== WebSocketMessageType.Close)
                {
                    await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }
    }
}
