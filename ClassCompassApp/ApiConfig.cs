using System;

namespace ClassCompass
{
    public static class ApiConfig
    {
        // IMPORTANT: Use HTTP not HTTPS for local development
        public const string BaseUrl = "http://192.168.68.83:5004";
        
        // Alternative IPs to try if above doesn't work:
        // public const string BaseUrl = "http://192.168.1.100:5004";
        // public const string BaseUrl = "http://10.0.0.100:5004";
        
        public static class Endpoints
        {
            public const string Students = "/api/Student";
            public const string Teachers = "/api/Teacher";
            public const string ClassRooms = "/api/ClassRoom";
            public const string Attendance = "/api/Attendance";
            public const string Schools = "/api/School";
            public const string Health = "/health";
            public const string Login = "/api/Auth/Login";
        }
        
        // Get full URL for endpoint
        public static string GetFullUrl(string endpoint)
        {
            return BaseUrl.TrimEnd('/') + "/" + endpoint.TrimStart('/');
        }
    }
}


