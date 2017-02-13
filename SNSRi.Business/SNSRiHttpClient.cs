using System.Net.Http;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public class SNSRiHttpClient : IHttpClient
    {
        public string GetStringAsync(string requestUri)
        {
            using (var httpClient = new HttpClient())
            {
                return httpClient.GetStringAsync(requestUri).Result;
            }
        }
    }
}