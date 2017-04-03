#define AZURE

#if AZURE

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuth.Interfaces
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(IMobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null);
        Task<bool> RefreshUser(IMobileServiceClient client);
        void ClearCookies();
    }
}

#endif