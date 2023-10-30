namespace Domain.Options
{
    public class Secrets
    {
        public string DatabaseConnectionString { get; set; } = string.Empty;
        public PasswordSecrets Password { get; set; }
    }
}
