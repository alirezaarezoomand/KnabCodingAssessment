namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi
{
    public class ApiConfiguration
    {
        public string[] ApiVersions { get; set; }
        public string ApiDefaultVersion { get; set; }
        public string[] ApiSupportedLanguages { get; set; }
        public string ApiDefaultLanguage { get; set; }
        public string ApiName { get; set; }
        public string ApiBaseUrl { get; set; }
        public bool IsSwaggerEnabled { get; set; }
        public bool CorsAllowAnyOrigin { get; set; }
        public string[] CorsAllowOrigins { get; set; } = new List<string>().ToArray();
        public int TimeoutInMilliseconds { get; set; }
    }
}
