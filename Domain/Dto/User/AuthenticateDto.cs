namespace Domain.Dto.User
{
    public class AuthenticateDto
    {
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
