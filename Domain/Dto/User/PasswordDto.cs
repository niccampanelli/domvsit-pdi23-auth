namespace Domain.Dto.User
{
    public class PasswordDto
    {
        public long UserId { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
