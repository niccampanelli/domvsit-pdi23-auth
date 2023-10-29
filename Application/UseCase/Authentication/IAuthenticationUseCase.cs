using Domain.Dto.User;

namespace Application.UseCase.Authentication
{
    public interface IAuthenticationUseCase
    {
        Task<UserDto> Create(UserDto input);
    }
}
