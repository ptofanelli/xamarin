using SocialLogin.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialLogin;
using Microsoft.WindowsAzure.MobileServices;
using SocialLogin.Helpers;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationImpl))]
namespace SocialLogin.UWP
{
    public class AuthenticationImpl : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
