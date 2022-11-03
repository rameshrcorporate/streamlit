namespace Gateway.API.Common
{
    public class ApiSettings
    {
        public string? IAMAPI { get; set; }
        public string? ProductAPI { get; set; }
        public string? HostUrl { get; set; }
    }
    
    public class HostSettings
    {
        public string? HostUrl { get; set; } 
    }

    public static class IAMApiEndPoints
    {
        public const string User = "/api/iam/";
    }
}
