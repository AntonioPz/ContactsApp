using ContactsApp.Models;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace ContactsApp.ViewModels
{
    public class ContactDetailViewModel : ViewModelBase
    {
        private Person person;

        public Person Person
        {
            get => person;
            set => SetProperty(ref person, value);
        }
        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get => locations;
            set => SetProperty(ref locations, value);
        }
        public ContactDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Detalles de contacto";
            Locations = new ObservableCollection<Location>();
        }
        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters["personId"] is int personId)
            {
                Person = await App.PersonRepo.GetAsync(personId);

                var position = new Position(double.Parse(Person.Latitude), double.Parse(Person.Longitude)); // Latitude, Longitude

                Locations = new ObservableCollection<Location>() { new Location() { Position = position, Address = Person.City, Description = Person.Street} };
            }
        }
    }
}
