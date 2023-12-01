using Domain.Dto.User;

namespace Domain.Repository
{
    public interface IRefreshTokenRepository
    {
        Task RegisterRefreshTokenSession(RefreshTokenDto input);
        Task<bool> IsRefreshTokenRegistered(RefreshTokenDto input);
        Task RemoveRegisteredUserRefreshTokens(long userId);
    }
}
