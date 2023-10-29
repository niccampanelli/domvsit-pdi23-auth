using Domain.Dto.User;
using Domain.Repository;
using Infrastructure.Setup;
using Domain.Mappers.User;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UserDto> Create(UserDto input)
        {
            var entity = input.MapToEntity();
            var result = _databaseContext.Users.Add(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }
    }
}
