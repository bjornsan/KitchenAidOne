using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace KitchenAid.App.Services
{
    public class UserNotification
    {
        public static async void NotifyUser(string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "Something unexcpected failed, please try again.";

            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
}
