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
        Task<UserDto> GetUserById(long id);
        Task SetNewPassword(PasswordDto input);
        Task RemoveRegisteredUserRefreshTokens(long userId);
        Task RemoveRegisteredAttendantRefreshTokens(long attendantId);
        Task RegisterRefreshTokenSession(RefreshTokenDto input);
        string EncryptPassword(string password);
        string GenerateToken(long id, TokenUserTypeEnum userType);
        string GenerateRefreshToken();
        ExtractIdFromTokenOutputDto ExtractIdFromToken(string token);
    }
}
