using Domain.Dto.User;
using Domain.Options;
using Domain.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.UseCase.Authentication
{
    public class AuthenticationUseCase : IAuthenticationUseCase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<Secrets> _secrets;

        public AuthenticationUseCase(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IOptions<Secrets> secrets)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _secrets = secrets;
        } 

        public async Task<UserDto> Authenticate(AuthenticateDto input)
        {
            return await _userRepository.Authenticate(input);
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _userRepository.VerifyEmailInUse(email);
        }

        public async Task<bool> ValidateToken(string token)
        {
            var secret = _secrets.Value.Authentication.TokenSecret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidation = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false
            });

            return tokenValidation.IsValid;
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var secret = _secrets.Value.Authentication.RefreshTokenSecret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidation = await tokenHandler.ValidateTokenAsync(refreshToken, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false,
                ValidateAudience = false,
                ValidateIssuer = false
            });

            return tokenValidation.IsValid;
        }

        public long ExtractIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var idClaim = jwtToken.Claims.First(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            var id = long.Parse(idClaim);
            return id;
        }
        
        public async Task<bool> IsRefreshTokenRegistered(RefreshTokenDto input)
        {
            return await _refreshTokenRepository.IsRefreshTokenRegistered(input);
        }

        public async Task<UserDto> CreateUser(UserDto input)
        {
            return await _userRepository.Create(input);
        }

        public async Task SetNewPassword(PasswordDto input)
        {
            await _userRepository.SetNewPassword(input);
        }

        public async Task RemoveRegisteredUserRefreshTokens(long userId)
        {
            await _refreshTokenRepository.RemoveRegisteredUserRefreshTokens(userId);
        }

        public async Task RegisterRefreshTokenSession(RefreshTokenDto input)
        {
            await _refreshTokenRepository.RegisterRefreshTokenSession(input);
        }

        public string EncryptPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{_secrets.Value.Password.StartSalt}{password}{_secrets.Value.Password.EndSalt}");
            var hash = SHA512.HashData(bytes);

            var encrypted = new StringBuilder();

            foreach (var b in hash)
            {
                encrypted.Append(b.ToString("X2"));
            }

            return encrypted.ToString();
        }

        public string GenerateToken(long id)
        {
            var secret = _secrets.Value.Authentication.TokenSecret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var expirationInMinutes = _secrets.Value.Authentication.TokenExpirationInMinutes;

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var secret = _secrets.Value.Authentication.RefreshTokenSecret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var expirationInMinutes = _secrets.Value.Authentication.RefreshTokenExpirationInMinutes;

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
