using Domain.Dto.User;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserDto> Create(UserDto input);
    }
}
