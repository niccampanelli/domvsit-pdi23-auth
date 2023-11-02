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
        private readonly IUserRepository _userRepository;
        private readonly IOptions<Secrets> _secrets;

        public AuthenticationUseCase(IUserRepository userRepository, IOptions<Secrets> secrets)
        {
            _userRepository = userRepository;
            _secrets = secrets;
        } 

        public async Task<UserDto> Authenticate(string email, string encryptedPassword)
        {
            return await _userRepository.Authenticate(email, encryptedPassword);
        }

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _userRepository.VerifyEmailInUse(email);
        }

        public async Task<bool> ValidateAuthenticationToken(string token)
        {
            var secret = _secrets.Value.Authentication.Secret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidation = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
            });

            return tokenValidation.IsValid;
        }

        public async Task<UserDto> CreateUser(UserDto input)
        {
            return await _userRepository.Create(input);
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
            var secret = _secrets.Value.Authentication.Secret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Sid, id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
