﻿using Domain.Dto.User;
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

        public async Task<UserDto> Authenticate(string email, string encryptedPassword)
        {
            var authenticatedUser = await _databaseContext.Users.FirstAsync(u => u.Email == email && u.Password == encryptedPassword);
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
    }
}
