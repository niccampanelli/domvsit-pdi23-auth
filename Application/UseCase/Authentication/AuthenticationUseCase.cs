using Domain.Dto.User;
using Domain.Repository;

namespace Application.UseCase.Authentication
{
    public class AuthenticationUseCase : IAuthenticationUseCase
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(UserDto input)
        {
            return await _userRepository.Create(input);
        }
    }
}
