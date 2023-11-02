using Domain.Dto.User;

namespace Application.UseCase.Authentication
{
    public interface IAuthenticationUseCase
    {
        Task<UserDto> Authenticate(string email, string encryptedPassword);
        Task<bool> VerifyEmailInUse(string email);
        Task<bool> ValidateAuthenticationToken(string token);
        Task<UserDto> CreateUser(UserDto input);
        string EncryptPassword(string password);
        string GenerateToken(long id);
    }
}
