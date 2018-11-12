﻿using System;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Messaging;

using StreamKontrolUnit.Activation;

using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;

namespace StreamKontrolUnit.Services
{
    // More about adding push notifications to your Windows app at https://docs.microsoft.com/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started-push
    internal class AzureNotificationsService : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public async Task InitializeAsync()
        {
            // TODO WTS: Set your Hub Name
            var hubName = string.Empty;

            // TODO WTS: Set your DefaultListenSharedAccessSignature
            var accessSignature = string.Empty;

            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub(hubName, accessSignature);
            var result = await hub.RegisterNativeAsync(channel.Uri);
            if (result.RegistrationId != null)
            {
                // Registration was successful
            }

            // You can also send push notifications from Windows Developer Center targeting your app consumers
            // More details at https://docs.microsoft.com/windows/uwp/publish/send-push-notifications-to-your-apps-customers
        }

        protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
        {
            // TODO WTS: Handle activation from toast notification,
            // Be sure to use the template 'ToastGeneric' in the toast notification configuration XML
            // to ensure OnActivated is called when launching from a Toast Notification sent from Azure
            //
            // More about handling activation at https://docs.microsoft.com/windows/uwp/design/shell/tiles-and-notifications/send-local-toast
            await Task.CompletedTask;
        }
    }
}
