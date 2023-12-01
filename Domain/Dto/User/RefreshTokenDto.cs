namespace Domain.Dto.User
{
    public class RefreshTokenDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Value { get; set; }
    }
}
