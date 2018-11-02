using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IHttpService
    {
        Task<HttpClient> GetClient(string adress);
    }
}