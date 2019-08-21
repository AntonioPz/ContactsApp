using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ContactsApp.Bussiness
{
    public class UserProcess
    {
        internal async Task CreateSession(string email)
        {
            try
            {
                await SecureStorage.SetAsync("Email", email);
                App.IsLogged = true;
            }
            catch (Exception ex)
            {
            }
        }
        internal static async Task<bool> IsLoggued()
        {
            bool result = false;
            try
            {
                string email = await SecureStorage.GetAsync("Email");
                if (!string.IsNullOrEmpty(email))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        internal void Logout()
        {
            try
            {
                SecureStorage.RemoveAll();
                App.IsLogged = false;
            }
            catch (Exception ex)
            {
            }
        }

    }
}
