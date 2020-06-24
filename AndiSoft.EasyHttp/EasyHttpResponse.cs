using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AndiSoft.EasyHttp
{
    /// <summary>
    /// Inherits HttpResponseMessage.
    /// </summary>
    public class EasyHttpResponse
    {
        public string StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        /// <summary>
        /// Request result as string.
        /// </summary>
        public string StringResult { get; set; }

        internal EasyHttpResponse()
        {
            
        }

        internal EasyHttpResponse(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode.ToString();
            ReasonPhrase = response.ReasonPhrase;
            IsSuccessStatusCode = response.IsSuccessStatusCode;
            StringResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// Returns status code and reason phrase formatted string
        /// </summary>
        /// <returns></returns>
        public string GetStatusString()
        {
            return $"{StatusCode}: {ReasonPhrase}";
        }
    }

    /// <summary>
    /// HttpResponseMessage with parsed object inside. If parse fails, check the ParseException attribute.
    /// </summary>
    /// <typeparam name="T">Desired ObjectResult type.</typeparam>
    public class EasyHttpResponse<T> : EasyHttpResponse
    {
        /// <summary>
        /// Parse status.
        /// </summary>
        public bool ParseSuccess { get; }
        /// <summary>
        /// Parse exception, in case o failure.
        /// </summary>
        public Exception ParseException { get; set; }
        /// <summary>
        /// Specified type object result.
        /// </summary>
        public T ObjectResult { get; }

        internal EasyHttpResponse() { }

        internal EasyHttpResponse(EasyHttpResponse response)
        {
            StatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            IsSuccessStatusCode = response.IsSuccessStatusCode;
            StringResult = response.StringResult;

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;

            try
            {
                ObjectResult = JsonConvert.DeserializeObject<T>(StringResult, jsonSettings);
                ParseSuccess = true;
            }
            catch (Exception ex)
            {
                ParseSuccess = false;
                ParseException = ex.GetBaseException();
            }
        }
    }
}
