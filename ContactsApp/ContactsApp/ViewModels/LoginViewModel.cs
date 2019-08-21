using ContactsApp.Bussiness;
using ContactsApp.Data;
using ContactsApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand LoginCommand { get; private set; }
        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Login";
            LoginCommand = new DelegateCommand(Login);
          
        }

        private async void Login()
        {
            Bussiness.Result validation = UserValidation.ValidateCredentials(Email, Password);
            if (validation.IsValid)
            {
                LoginDTO loginDTO = await App.DataService.Login(Email, Password);
                if (!string.IsNullOrEmpty(loginDTO.Token))
                {
                    UserProcess userProcess = new UserProcess();
                    await userProcess.CreateSession(Email);
                    await NavigationService.NavigateAsync(new Uri(string.Format("http://myapp.com/{0}/{1}", nameof(NavigationPage), nameof(ContactsListPage)), UriKind.Absolute));
                }
                else await PageDialogService.DisplayAlertAsync("Acceso denegado", "El usuario o contraseña son incorrectos", "Ok");
            }
            else await PageDialogService.DisplayAlertAsync("Datos incorrectos", validation.Message, "Ok");
        }
    }
}
