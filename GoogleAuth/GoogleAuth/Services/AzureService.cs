using GoogleAuth.Helpers;
using GoogleAuth.Interfaces;
using GoogleAuth.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace GoogleAuth.Services
{
    public class AzureService
    {
        public MobileServiceClient Client { get; set; } = null;
        public static bool UseAuth { get; set; } = false;

        public async Task Initialize()
        {
            if (Client?.SyncContext.IsInitialized ?? false)
                return;
            
            var appUrl = "https://newsv1.azurewebsites.net";

#if AUTH
            Client = new MobileServiceClient(appUrl, new AuthHandler());
                if (!string.IsNullOrWhiteSpace (Settings.AuthToken) && !string.IsNullOrWhiteSpace (Settings.UserId))
                {
                    Client.CurrentUser = new MobilseServiceClient(Settings.UserId);
                    Client.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
                }
#else
            //Create client
            Client = new MobileServiceClient(appUrl);
#endif

        }

        public async Task<bool> LoginAsync()
        {
            await Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Google);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Login error", "Unable to login, please try again", "OK");
                });
                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }
            return true;
        }

    }
}
