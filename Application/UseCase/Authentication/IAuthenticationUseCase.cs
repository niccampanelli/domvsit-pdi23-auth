using Domain.Dto.User;

namespace Application.UseCase.Authentication
{
    public interface IAuthenticationUseCase
    {
        string EncryptPassword(string password);
        Task<bool> VerifyEmailInUse(string email);
        Task<UserDto> CreateUser(UserDto input);
    }
}
