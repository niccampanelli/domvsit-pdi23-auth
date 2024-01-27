using Domain.Dto.User;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserDto> Authenticate(AuthenticateDto input);
        Task<bool> VerifyEmailInUse(string email);
        Task<UserDto> Create(UserDto input);
        Task<UserDto> GetById(long id);
        Task SetNewPassword(PasswordDto input);
    }
}
