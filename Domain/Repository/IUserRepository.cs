using Domain.Dto.User;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserDto> Authenticate(string email, string encryptedPassword);
        Task<bool> VerifyEmailInUse(string email);
        Task<UserDto> Create(UserDto input);
    }
}
