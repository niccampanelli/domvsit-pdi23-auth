using Domain.Dto.User;

namespace Application.UseCase.Authentication
{
    public interface IAuthenticationUseCase
    {
        Task<UserDto> Authenticate(AuthenticateDto input);
        Task<bool> VerifyEmailInUse(string email);
        Task<bool> ValidateToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<bool> IsRefreshTokenRegistered(RefreshTokenDto input);
        Task<UserDto> CreateUser(UserDto input);
        Task SetNewPassword(PasswordDto input);
        Task RemoveRegisteredUserRefreshTokens(long userId);
        Task RegisterRefreshTokenSession(RefreshTokenDto input);
        string EncryptPassword(string password);
        string GenerateToken(long id);
        string GenerateRefreshToken();
        long ExtractIdFromToken(string token);
    }
}
