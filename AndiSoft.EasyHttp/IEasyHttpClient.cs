using System.Net.Http;

namespace AndiSoft.EasyHttp
{
    public interface IEasyHttpClient
    {
        EasyHttpResponse Get(string url);
        EasyHttpResponse<T> Get<T>(string url);
        EasyHttpResponse Post(string url, string body, string contentType = null);
        EasyHttpResponse PostJson(string url, object body);
        EasyHttpResponse<T> PostJson<T>(string url, object body);
        EasyHttpResponse Put(string url, string body, string contentType = null);
        EasyHttpResponse PutJson(string url, object body);
        EasyHttpResponse<T> PutJson<T>(string url, object body);
        EasyHttpResponse Delete(string url);
    }
}