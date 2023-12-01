﻿using Domain.Dto.User;
using Domain.Mappers.User;
using Domain.Repository;
using Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private DatabaseContext _databaseContext;

        public RefreshTokenRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task RegisterRefreshTokenSession(RefreshTokenDto input)
        {
            var entity = input.MapToEntity();
            await _databaseContext.RefreshTokens.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<bool> IsRefreshTokenRegistered(RefreshTokenDto input)
        {
            return await _databaseContext.RefreshTokens.AnyAsync(r => r.UserId == input.UserId && r.Value == input.Value);
        }
    }
}
