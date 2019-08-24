using ContactsApp.Helpers;
using ContactsApp.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public class DataService 
    {
        HttpClient _client;

        public Person Person { get; private set; }

        public DataService()
        {
            _client = new HttpClient();
        }

        public async Task<LoginDTO> Login(string email, string password)
        {
            LoginDTO loginDTO = new LoginDTO();
            var uri = new Uri(Constants.LOGIN_URL);
            try
            {
                var response = await _client.PostAsync(uri,new StringContent(JsonConvert.SerializeObject(new { email, password }),Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    loginDTO = JsonConvert.DeserializeObject<LoginDTO>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return loginDTO;
        }

        public async Task GetPeopleAsync(int count)
        {
            var uri = new Uri(string.Format("{0}/?results={1}", Constants.PEOPLE_URL, count.ToString()));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<PeopleDTO>(content);
                    var people = (from item in result.Results
                                  select new Person()
                                  {
                                      FullName = string.Format("{0} {1} {2}", item.Name.Title, item.Name.First, item.Name.Last),
                                      City = item.Location.City,
                                      ImageURL = item.Picture.Medium,
                                      Email = item.Email,
                                      Latitude = item.Location.Coordinates.Latitude,
                                      Longitude = item.Location.Coordinates.Longitude,
                                      PhoneNumber = item.Phone,
                                      PostalCode = Convert.ToInt32(item.Location.Postcode),
                                      Rating = Operations.GenerateRating(),
                                      State = item.Location.State,
                                      Street = item.Location.Street
                                  });
                    foreach (var person in people)
                    {
                        await App.PersonRepo.AddAsync(person);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

    }
}
