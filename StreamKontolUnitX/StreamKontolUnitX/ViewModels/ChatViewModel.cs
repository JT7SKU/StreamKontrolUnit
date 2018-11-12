using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using StreamKontolUnitX.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StreamKontolUnitX.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        HubConnection hubConnection;

        public ChatMessage ChatMessage { get; }
        public ObservableRangeCollection<ChatMessage> Messages { get; }
        bool isConnected;
        public bool IsConnected
        {
            get => isConnected; set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetProperty(ref isConnected, value);
                });
            }
        }
        public Command SendMessageCommand { get; }
        public Command ConnectedCommand { get; }
        public Command DisconnectCommand { get; set; }

        Random random;
        public ChatViewModel()
        {
            ChatMessage = new ChatMessage();
            Messages = new ObservableRangeCollection<ChatMessage>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectedCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            random = new Random();
            string signalRUrl = "https://localhost:5001";
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

        private async Task Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }
            
             await hubConnection.StopAsync();
            IsConnected = false;
            SendLocalMessage("Disconnected...");         
            
        }

        private async Task Connect()
        {
            if (IsConnected)
            {
                return;
            }
            try
            {
                await hubConnection.StartAsync();
                IsConnected = true;
                SendLocalMessage("Connected...");
            }
            catch (Exception ex)
            {

                SendLocalMessage($"Connection error: {ex.Message}");
            }
        }

        public void SendLocalMessage(string message)
        {
            Device.BeginInvokeOnMainThread(()=>
                {
                    Messages.Add(new ChatMessage
                    {
                        Message = message
                    });
                });
        }

        private async Task SendMessage()
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
