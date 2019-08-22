using ContactsApp.Bussiness;
using ContactsApp.Models;
using ContactsApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class ContactsListViewModel : ViewModelBase
    {
        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get => people;
            set => SetProperty(ref people, value);
        }

        private Person person;
        public Person Person
        {
            get => person;
            set => SetProperty(ref person, value);
        }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand TapCommand { get; private set; }
        public Command LoadPeopleCommand { get; private set; }

        public ContactsListViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Contactos";
            People = new ObservableCollection<Person>();
            LogoutCommand = new DelegateCommand(Logout);
            TapCommand = new DelegateCommand(PersonTapped);
            LoadPeopleCommand = new Command(async () => await LoadPeople());
            LoadPeopleCommand.Execute(null);
        }

        private async Task LoadPeople()
        {
            await App.DataService.GetAndSavePeople(5);
            People = await App.PersonRepo.GetLast(5);
        }


        private async void PersonTapped()
        {
            NavigationParameters valuePairs = new NavigationParameters
            {
                { "personId", Person.Id }
            };
            Person = new Person();
            await NavigationService.NavigateAsync(nameof(ContactDetailPage), valuePairs);
        }

        private async void Logout()
        {
            UserProcess userProcess = new UserProcess();
            userProcess.Logout();
            await NavigationService.NavigateAsync(new Uri(string.Format("http://myapp.com/{0}/{1}", nameof(NavigationPage), nameof(LoginPage)), UriKind.Absolute));
        }

    }
}
