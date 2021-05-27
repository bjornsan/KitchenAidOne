using System;
using Windows.UI.Popups;

namespace KitchenAid.App.Services
{
    /// <summary>Helper service to send messages to the user</summary>
    public class UserNotification
    {
        /// <summary>Creates a MessageDialog and displays it async.</summary>
        /// <param name="message">The message.</param>
        public static async void NotifyUser(string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "Something unexcpected failed, please try again.";

            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
