using ContactsApp.Helpers;
using ContactsApp.Models;
using ContactsApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public class DataService : IRestService<PeopleDTO>
    {
        HttpClient _client;
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

        public async Task<PeopleDTO> GetPeople(int count)
        {
            PeopleDTO result = new PeopleDTO();

            var uri = new Uri(string.Format("{0}/?results={1}", Constants.PEOPLE_URL, count.ToString()));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<PeopleDTO>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return result;
        }
    }
}
