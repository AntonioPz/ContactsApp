using ContactsApp.Models;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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

        public ContactDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            Title = "Detalles de contacto";
        }
        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            if (parameters["personId"] is int personId)
            {
                Person = await App.PersonRepo.GetAsync(personId);

                var position = new Position(double.Parse(Person.Latitude), double.Parse(Person.Longitude)); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = "custom pin",
                    Address = "custom detail info"
                };

            }
        }
    }
}
