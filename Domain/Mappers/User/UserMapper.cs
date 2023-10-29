using Domain.Dto.User;
using Domain.Entities.User;

namespace Domain.Mappers.User
{
    public static class UserMapper
    {
        public static UserEntity MapToEntity(this UserDto input)
        {
            return new UserEntity()
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Password = input.Password
            };
        }

        public static UserDto MapToDto(this UserEntity input)
        {
            return new UserDto()
            {
                Id = input.Id,
                Name = input.Name,
                Email = input.Email,
                Password = input.Password
            };
        }
    }
}
