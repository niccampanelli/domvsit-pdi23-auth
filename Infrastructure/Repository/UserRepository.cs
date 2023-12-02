using Domain.Dto.User;
using Domain.Repository;
using Infrastructure.Setup;
using Domain.Mappers.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<UserDto> Authenticate(AuthenticateDto input)
        {
            var authenticatedUser = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(input.Email) && u.Password.Equals(input.EncryptedPassword));
            if (authenticatedUser == null)
                return default;

            return authenticatedUser.MapToDto();
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _databaseContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserDto> Create(UserDto input)
        {
            var entity = input.MapToEntity();
            var result = _databaseContext.Users.Add(entity);
            await _databaseContext.SaveChangesAsync();
            return result.Entity.MapToDto();
        }

        public async Task SetNewPassword(PasswordDto input)
        {
            var entity = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(input.UserId));
            entity.Password = input.EncryptedPassword;
            _databaseContext.Update(entity);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
