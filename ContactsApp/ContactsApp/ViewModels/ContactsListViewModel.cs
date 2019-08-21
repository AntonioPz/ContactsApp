using ContactsApp.Bussiness;
using ContactsApp.Models;
using ContactsApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            LogoutCommand = new DelegateCommand(Logout);
            TapCommand = new DelegateCommand(PersonTapped);
            LoadPeopleCommand = new Command(async () => await LoadPeople());
            LoadPeopleCommand.Execute(null);
        }

        private async Task LoadPeople()
        {
            try
            {
                var people = await App.DataService.GetPeople(5);
                int count = people.Results.Count;
                foreach (var item in people.Results)
                {
                    Person = new Person()
                    {
                        FullName = string.Format("{0} {1} {2}", item.Name.Title, item.Name.First, item.Name.Last),
                        City = item.Location.City,
                        ImageURL = item.Picture.Medium,
                        Email = item.Email,
                        Latitude = item.Location.Coordinates.Latitude,
                        Longitude = item.Location.Coordinates.Longitude,
                        PhoneNumber = item.Phone,
                        PostalCode = Convert.ToInt32(item.Location.Postcode),
                        Rating = GenerateRating(),
                        State = item.Location.State,
                        Street = item.Location.Street

                    };
                    await App.PersonRepo.AddAsync(Person);

                }
                People = new ObservableCollection<Person>(await App.PersonRepo.GetLastItems(5));
            }
            catch (Exception)
            {
            }
            
        }

        private double GenerateRating()
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += RandomNumber(1,6);
            }
            return (double)sum / 10;
        }
        Random random = new Random(Guid.NewGuid().GetHashCode());

        public int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        private async void PersonTapped()
        {
            NavigationParameters valuePairs = new NavigationParameters
            {
                { "personId", Person.Id }
            };
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
