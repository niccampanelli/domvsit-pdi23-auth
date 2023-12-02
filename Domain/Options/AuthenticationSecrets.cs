namespace Domain.Options
{
    public class AuthenticationSecrets
    {
        public string TokenSecret { get; set; } = string.Empty;
        public string RefreshTokenSecret { get; set; } = string.Empty;
        public int TokenExpirationInMinutes { get; set; } = 0;
        public int RefreshTokenExpirationInMinutes { get; set; } = 0;
    }
}
