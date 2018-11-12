using Microsoft.AspNetCore.SignalR.Client;
using StreamKontolUnitX.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamKontolUnitX.Services
{
    public class StreamKontrolUnit
    {
        bool IsConnected;
        Random random;
        HubConnection hubConnection;
        string signalRUrl = "https://localhost:5001";
        public StreamKontrolUnit()
        {
            random = new Random();
            hubConnection = new HubConnectionBuilder().WithUrl($"{signalRUrl}/hubs/chathub").Build();
            hubConnection.Closed += async (error) =>
            {
                SendLocalMessage("Connection Closed...");
                IsConnected = false;
                await Task.Delay(random.Next(0, 5) * 1000);
                await Connect();
            };
            hubConnection.On<string, string>("Receive Message", (user, message) =>
            {
                var finalMessage = $"{user} says {message}";
                SendLocalMessage(finalMessage);
            });
        }

        public bool IsBusy { get; private set; }

        public async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await hubConnection.InvokeAsync("SendMessage", ChatMessage.User, ChatMessage.Message);
            }
            catch (Exception ex)
            {

                SendLocalMessage($"Send failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
