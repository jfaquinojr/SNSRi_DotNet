using System;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public interface IHttpClient
    {
        string GetStringAsync(string requestUri);
    }
}