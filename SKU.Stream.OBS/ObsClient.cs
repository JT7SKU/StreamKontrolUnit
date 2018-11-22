using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace SKU.Stream.OBS
{
    public class OBSClient : IDisposable
    {
        public const string LOGGER_CATEGORY = "SKU.Stream.OBS";
        private WebSocket webSocket;
        //websocket ServerUrl: localhost port: 4444
       private static string ServerUrl = "localhost";
       private static int Port = 4444;
        string Url = $"ws://{ServerUrl}:{Port}";

        private StreamReader inputStream;
        private StreamWriter outputStream;
        private int _Retries;
        private Task _ReceiveMessagesTask;
        private MemoryStream _RecieveStream = new MemoryStream();

        public OBSClient(IOptions<ConfigurationSettings> settings, ILoggerFactory loggerFactory): this(settings.Value, loggerFactory.CreateLogger(LOGGER_CATEGORY))
        {

        }
        internal OBSClient(ConfigurationSettings settings, ILogger logger)
        {
            this.Settings = settings;
            this.Logger = logger;
        }

        ~OBSClient()
        {
            Logger.LogError("QC the OBSClient");
            Dispose(false);
        }
        public void Init()
        {
            Connect();


        }
        public ConfigurationSettings Settings { get; }
        public ILogger Logger { get; }
        private readonly CancellationTokenSource _Shutdown;

            private async void Connect()
        {
            var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri(Url),CancellationToken.None);

        }
        protected string HasEncode(string input)
        {
            var sha256 = new SHA256Managed();
            byte[] textBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(textBytes);

            return System.Convert.ToBase64String(hash);
        }
        protected string NewMessageID(int lenght = 16)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMOPQRSTUVWXYZ";
            var random = new Random();

            string result = "";
            for (int i = 0; i < lenght; i++)
            {
                int index = random.Next(0, pool.Length - 1);
                result += pool[index];
            }
            return result;
        }
        #region IDisposable Support
        private bool disposedValue = false;
        private Thread _ReceiveMessagesThread;

        public virtual void Dispose(bool disposing)
        {
            Logger.LogWarning("Disposing of OBSClient");
            if (!disposedValue)
            {
                if (disposing)
                {
                    _Shutdown.Cancel();
                }
                webSocket.Dispose();
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
#endregion

    }
    //public static class BufferHelpers
    //{
    //    public static ArraySegment<byte> ToBuffer(this string Text)
    //    {
    //        return Encoding.UTF8.GetBytes(Text);
    //    }
    //}
}
