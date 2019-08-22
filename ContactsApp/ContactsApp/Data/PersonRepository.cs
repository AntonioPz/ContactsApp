using ContactsApp.Models;
using ContactsApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public class PersonRepository : IDataRepository<Person>
    {
        SQLiteAsyncConnection conn;
        public string StatusMessage { get; set; }

        public PersonRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>().Wait();
        }

        public async Task<int> AddAsync(Person item)
        {
            int result = 0;
            try
            {
                result = await conn.InsertAsync(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(StatusMessage);
                StatusMessage = string.Format("Failed to add. Error: {0}", ex.Message);
            }
            return result;
        }

        public async Task<int> UpdateAsync(Person item)
        {
            int result = 0;
            try
            {
                result = await conn.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update. Error: {0}", ex.Message);
            }
            return result;
        }

        public async Task<int> DeleteAsync(Person item)
        {
            int result = 0;
            try
            {
                result = await conn.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete. Error: {0}", ex.Message);
            }
            return result;
        }

        public async Task<Person> GetAsync(int id)
        {
            Person result = null;
            try
            {
                result = await conn.GetAsync<Person>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to get. Error: {0}", ex.Message);
            }
            return result;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            try
            {
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. Error: {0}", ex.Message);
            }

            return new List<Person>();
        }
        public async Task<List<Person>> GetLastItems(int count)
        {
            try
            {
                return await conn.Table<Person>().OrderByDescending(p => p.Id).Take(count).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. Error: {0}", ex.Message);
            }

            return new List<Person>();
        }
        public async Task<ObservableCollection<Person>> GetLast(int count)
        {
            List<Person> list = new List<Person>();
            try
            {
                list = await conn.Table<Person>().OrderByDescending(p => p.Id).Take(count).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. Error: {0}", ex.Message);
            }

            return new ObservableCollection<Person>(list);
        }



        public async Task<List<Person>> GetGroup(int count)
        {
            await Task.Yield();
            return new List<Person>();
        }
    }
}