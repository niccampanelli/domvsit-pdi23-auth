using Domain.Dto.User;
using Domain.Entities.User;

namespace Domain.Mappers.User
{
    public static class RefreshTokenMapper
    {
        public static RefreshTokenEntity MapToEntity(this RefreshTokenDto input)
        {
            return new RefreshTokenEntity()
            {
                UserId = input.UserId,
                Value = input.Value
            };
        }
    }
}
