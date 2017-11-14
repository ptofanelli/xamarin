using Microsoft.WindowsAzure.MobileServices;
using SocialLogin.Helpers;
using SocialLogin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]
namespace SocialLogin.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "https://ptofanelli.azurewebsites.net";

        public MobileServiceClient Client { get; private set; } = null;

        private void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            //TODO verifica se usuario ja esta logado
            /*
            if (!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
            */
        }

        public async Task<MobileServiceUser> LoginAsync()
        {
            Initialize();
            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            return user;
        }

    }
}
