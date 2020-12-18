using System.Threading.Tasks;
#pragma warning disable 1591

namespace AndiSoft.EasyHttp
{
    public interface IEasyHttpClient
    {
        EasyHttpResponse Get(string url);
        Task<EasyHttpResponse> GetAsync(string url);
        EasyHttpResponse<T> Get<T>(string url);
        Task<EasyHttpResponse<T>> GetAsync<T>(string url);
        EasyHttpResponse Post(string url, string body, string contentType = null);
        Task<EasyHttpResponse> PostAsync(string url, string body, string contentType = null);
        EasyHttpResponse PostJson(string url, object body);
        Task<EasyHttpResponse> PostJsonAsync(string url, object body);
        EasyHttpResponse<T> PostJson<T>(string url, object body);
        Task<EasyHttpResponse<T>> PostJsonAsync<T>(string url, object body);
        EasyHttpResponse Put(string url, string body, string contentType = null);
        Task<EasyHttpResponse> PutAsync(string url, string body, string contentType = null);
        EasyHttpResponse PutJson(string url, object body);
        Task<EasyHttpResponse> PutJsonAsync(string url, object body);
        EasyHttpResponse<T> PutJson<T>(string url, object body);
        Task<EasyHttpResponse<T>> PutJsonAsync<T>(string url, object body);
        EasyHttpResponse Delete(string url);
        Task<EasyHttpResponse> DeleteAsync(string url);
    }
}