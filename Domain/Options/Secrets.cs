namespace Domain.Options
{
    public class Secrets
    {
        public AuthenticationSecrets Authentication { get; set; }
        public string DatabaseConnectionString { get; set; } = string.Empty;
        public PasswordSecrets Password { get; set; }
    }
}
