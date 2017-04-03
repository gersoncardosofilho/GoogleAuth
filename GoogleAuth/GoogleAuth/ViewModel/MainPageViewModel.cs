using GoogleAuth.Helpers;
using GoogleAuth.Interfaces;
using GoogleAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoogleAuth.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        AzureService azureService;

        public MainPageViewModel()
        {
            azureService = DependencyService.Get<AzureService>();
        }

        string loadingMessage;
        public string LoadingMessage
        {
            get
            {
                return loadingMessage;
            }
            set
            {
                if (loadingMessage == value)
                {
                    return;
                }
                loadingMessage = value;
                OnPropertyChanged();
            }
        }

        public Task<bool> LoginAsync()
        {

            if (Settings.IsLoggedIn)
            {
                return Task.FromResult(true);
            }
            else
            {
                return azureService.LoginAsync();
            }
        }

        ICommand loginAsyncCommand;
        public ICommand LoginAsyncCommand =>
            loginAsyncCommand
                ?? (loginAsyncCommand = new Command(async () => await ExecuteLoginAsyncCommand()));

        async Task ExecuteLoginAsyncCommand()
        {
            await LoginAsync();
        }
    }
}
