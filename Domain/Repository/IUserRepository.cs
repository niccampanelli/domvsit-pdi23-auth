using Domain.Dto.User;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task<bool> VerifyEmailInUse(string email);
        Task<UserDto> Create(UserDto input);
    }
}
