using Microsoft.WindowsAzure.MobileServices;
using SocialLogin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocialLogin.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private Command _loginFacebookCommand;
        public Command LoginFacebookCommand
        {
            get { return _loginFacebookCommand; }
            set { SetProperty<Command>(ref _loginFacebookCommand, value); }
        }

        private String _loginResultText;
        public String LoginResultText
        {
            get { return _loginResultText; }
            set { SetProperty<String>(ref _loginResultText, value); }
        }

        private AzureService AzureService;

        public LoginViewModel()
        {
            LoginFacebookCommand = new Command(ExecLoginFacebookCommand);
            AzureService = DependencyService.Get<AzureService>();
        }



        private async void ExecLoginFacebookCommand()
        {
            try
            {
                MobileServiceUser user = await AzureService.LoginAsync();
                LoginResultText = user?.UserId;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
