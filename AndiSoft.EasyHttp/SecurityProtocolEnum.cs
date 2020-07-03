namespace AndiSoft.EasyHttp
{
    public enum SecurityProtocolEnum 
    {
        /// <summary>
        /// Accepts TLS, TLS1.1 and TLS1.2 certifications
        /// </summary>
        Default,

        /// <summary>
        /// Bypass SSL certification
        /// </summary>
        BypassSSL,

        SSL3,
        TLS,
        TLS11,
        TLS12,
        TLS13
    }
}