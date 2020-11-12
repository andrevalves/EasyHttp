using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AndiSoft.EasyHttp
{
    public abstract class BaseHttpClient
    {
        protected readonly HttpClient EasyClient;

        #region Contructor

        /// <summary>
        /// Creates the client to make requests. The default Content-Type is 'application/json', which can be changed using the AddHeaders method.
        /// </summary>
        /// <param name="proxy">Optional proxy for requests. Default is null.</param>
        /// <param name="protocolType">Security Protocol to be used in the request. Default is TLS12</param>
        protected BaseHttpClient(WebProxy proxy = null, SecurityProtocolEnum protocolType = SecurityProtocolEnum.Default)
        {
            if (proxy == null)
            {
                proxy = new WebProxy();
            }

            var httpClientHandler = new HttpClientHandler
            {
                PreAuthenticate = true,
                UseDefaultCredentials = false,
                Proxy = proxy
            };

            #if !NET451
            if (protocolType == SecurityProtocolEnum.BypassSSL)
            {
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true;
            }
            #endif

            EasyClient = new HttpClient(httpClientHandler);
            EasyClient.DefaultRequestHeaders.Accept.Clear();
            EasyClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            switch (protocolType)
            {

                case SecurityProtocolEnum.BypassSSL:
                    //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
                    break;
                case SecurityProtocolEnum.SSL3:
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    break;
                case SecurityProtocolEnum.TLS:
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    break;
                case SecurityProtocolEnum.TLS11:
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                    break;
                case SecurityProtocolEnum.TLS12:
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    break;
                case SecurityProtocolEnum.TLS13:
                    break;
                case SecurityProtocolEnum.Default:
                default:
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    break;
            }
        }
        
        #endregion

        #region Headers

        /// <summary>
        /// Adds header to the request
        /// </summary>
        /// <param name="key">Header key.</param>
        /// <param name="value">Header value.</param>
        public void AddHeader(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return;

            if (key.ToLower().Equals("content-type"))
                EasyClient.DefaultRequestHeaders.Remove("Content-Type");

            EasyClient.DefaultRequestHeaders.Add(key, value);
        }

        /// <summary>
        /// Adds headers to the request
        /// </summary>
        /// <param name="headers">Headers to add.</param>
        public void AddHeaders(HttpRequestHeaders headers)
        {
            if (headers == null)
                return;

            foreach (var header in headers)
            {
                if (header.Key.ToLower().Equals("content-type"))
                    EasyClient.DefaultRequestHeaders.Remove("Content-Type");

                EasyClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Remove all headers.
        /// </summary>
        public void ClearHeadders()
        {
            EasyClient.DefaultRequestHeaders.Clear();
        }

        #endregion
    }
}