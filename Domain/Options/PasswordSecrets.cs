namespace Domain.Options
{
    public class PasswordSecrets
    {
        public string StartSalt { get; set; } = string.Empty;
        public string EndSalt { get; set; } = string.Empty;
    }
}
