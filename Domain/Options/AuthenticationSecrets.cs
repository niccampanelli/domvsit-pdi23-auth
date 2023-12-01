namespace Domain.Options
{
    public class AuthenticationSecrets
    {
        public string TokenSecret { get; set; } = string.Empty;
        public string RefreshTokenSecret { get; set; } = string.Empty;
    }
}
