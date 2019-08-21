using System.Threading.Tasks;

namespace ContactsApp.Services
{
    public interface IRestService<T>
    {
        Task<T> GetPeople(int count);
    }
}
