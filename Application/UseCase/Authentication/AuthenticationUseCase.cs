using Domain.Dto.User;
using Domain.Options;
using Domain.Repository;
using Microsoft.Extensions.Options;
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

        public async Task<bool> VerifyEmailInUse(string email)
        {
            return await _userRepository.VerifyEmailInUse(email);
        }

        public async Task<UserDto> CreateUser(UserDto input)
        {
            return await _userRepository.Create(input);
        }
    }
}
