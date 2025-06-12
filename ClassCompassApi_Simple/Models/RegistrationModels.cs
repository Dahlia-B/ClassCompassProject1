namespace ClassCompassApi_Simple.Models
{
    public class SchoolRegistrationRequest
    {
        public string Name { get; set; } = string.Empty;
        public int NumberOfClasses { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public School? School { get; set; }
    }
}
