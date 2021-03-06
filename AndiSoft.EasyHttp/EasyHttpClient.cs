﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AndiSoft.EasyHttp
{
    /// <summary>
    /// Class with some HTTP abstractions. It can read response content and convert it to specified objects.
    /// </summary>
    public class EasyHttpClient : BaseHttpClient, IEasyHttpClient
    {
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings();

        #region Contructor

        /// <summary>
        /// Creates the client to make requests. The default Content-Type is 'application/json', wich can be changed using the AddHeaders method.
        /// </summary>
        /// <param name="proxy">Optional proxy for requests. Default is null.</param>
        public EasyHttpClient(WebProxy proxy = null) : base(proxy)
        {
            _jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        /// <summary>
        /// Creates the client to make requests. The default Content-Type is 'application/json', wich can be changed using the AddHeaders method.
        /// </summary>
        /// <param name="securityProtocol">Security Protocol to be used in the request. Default is TLS 1.2 and 1.1.</param>
        /// <param name="proxy">Optional proxy for requests. Default is null.</param>
        public EasyHttpClient(SecurityProtocolEnum securityProtocol, WebProxy proxy = null) : base(proxy, securityProtocol)
        {
            _jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion

        #region GET

        /// <summary>
        /// Makes GET request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HttpResponseMessage</returns>
        public EasyHttpResponse Get(string url)
        {
            return GetAsync(url).Result;
        }

        /// <summary>
        /// Makes GET request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<EasyHttpResponse> GetAsync(string url)
        {
            var result = await EasyClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);
            return new EasyHttpResponse(result);
        }

        /// <summary>
        /// Makes GET request to the specified URL and converts response to the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the desired response.</typeparam>
        /// <param name="url"></param>
        /// <returns>Specified type. Will return an empty object on failure.</returns>
        public EasyHttpResponse<T> Get<T>(string url)
        {
            var result = Get(url);
            var response = new EasyHttpResponse<T>(result);

            return response;
        }

        /// <summary>
        /// Makes GET request to the specified URL and converts response to the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the desired response.</typeparam>
        /// <param name="url"></param>
        /// <returns>Specified type. Will return an empty object on failure.</returns>
        public async Task<EasyHttpResponse<T>> GetAsync<T>(string url)
        {
            var result = await GetAsync(url);
            var response = new EasyHttpResponse<T>(result);

            return response;
        }

        #endregion

        #region POST

        /// <summary>
        /// Makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <param name="contentType">Content type. Default is 'application/json'.</param>
        /// <returns></returns>
        public EasyHttpResponse Post(string url, string body, string contentType = null)
        {
            return PostAsync(url, body, contentType).Result;
        }

        /// <summary>
        /// Makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <param name="contentType">Content type. Default is 'application/json'.</param>
        /// <returns></returns>
        public async Task<EasyHttpResponse> PostAsync(string url, string body, string contentType = null)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = "application/json";

            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.Clear();
            httpContent.Headers.Add("content-type", contentType);

            return new EasyHttpResponse(await EasyClient.PostAsync(url, httpContent));
        }

        /// <summary>
        /// Converts body object to json and makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <returns></returns>
        public EasyHttpResponse PostJson(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            return Post(url, content);
        }

        /// <summary>
        /// Converts body object to json and makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <returns></returns>
        public async Task<EasyHttpResponse> PostJsonAsync(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            return await PostAsync(url, content);
        }

        /// <summary>
        /// Converts body object to json and makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST object content.</param>
        /// <returns>Response with parsed object in Result</returns>
        public EasyHttpResponse<T> PostJson<T>(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            var result = Post(url, content);
            
            return new EasyHttpResponse<T>(result);
            
        }

        /// <summary>
        /// Converts body object to json and makes POST request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST object content.</param>
        /// <returns>Response with parsed object in Result</returns>
        public async Task<EasyHttpResponse<T>> PostJsonAsync<T>(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            var result = await PostAsync(url, content);

            return new EasyHttpResponse<T>(result);
        }

        #endregion

        #region PUT

        /// <summary>
        /// Makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">PUT content.</param>
        /// <param name="contentType">Content type. Default is 'application/json'.</param>
        /// <returns></returns>
        public EasyHttpResponse Put(string url, string body, string contentType = null)
        {
            return PutAsync(url, body, contentType).Result;
        }

        /// <summary>
        /// Makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">PUT content.</param>
        /// <param name="contentType">Content type. Default is 'application/json'.</param>
        /// <returns></returns>
        public async Task<EasyHttpResponse> PutAsync(string url, string body, string contentType = null)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = "application/json";

            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.Clear();
            httpContent.Headers.Add("content-type", contentType);

            return new EasyHttpResponse(await EasyClient.PutAsync(url, httpContent));
        }

        /// <summary>
        /// Converts body object to json and makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <returns></returns>
        public EasyHttpResponse PutJson(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            return Put(url, content);
        }

        /// <summary>
        /// Converts body object to json and makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">POST content.</param>
        /// <returns></returns>
        public async Task<EasyHttpResponse> PutJsonAsync(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            return await PutAsync(url, content);
        }

        /// <summary>
        /// Converts body object to json and makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">PUT content.</param>
        /// <returns>Response with parsed object in Result</returns>
        public EasyHttpResponse<T> PutJson<T>(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            var result = Put(url, content);

            return new EasyHttpResponse<T>(result);
        }

        /// <summary>
        /// Converts body object to json and makes PUT request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body">PUT content.</param>
        /// <returns>Response with parsed object in Result</returns>
        public async Task<EasyHttpResponse<T>> PutJsonAsync<T>(string url, object body)
        {
            var content = body is string ? body.ToString() : JsonConvert.SerializeObject(body);
            var result = await PutAsync(url, content);

            return new EasyHttpResponse<T>(result);
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Makes DELETE request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HttpResponseMessage</returns>
        public EasyHttpResponse Delete(string url)
        {
            return new EasyHttpResponse(EasyClient.DeleteAsync(url).GetAwaiter().GetResult());
        }

        /// <summary>
        /// Makes DELETE request to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<EasyHttpResponse> DeleteAsync(string url)
        {
            return new EasyHttpResponse(await EasyClient.DeleteAsync(url));
        }

        #endregion

        #region Private Methods

        private EasyHttpResponse GetResponse(HttpResponseMessage response)
        {
            var result = JsonConvert.SerializeObject(response);
            return JsonConvert.DeserializeObject<EasyHttpResponse>(result);
        }

        #endregion
    }
}