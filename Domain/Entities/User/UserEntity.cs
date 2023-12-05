using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User
{
    [Table("users")]
    public class UserEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public RefreshTokenEntity? RefreshToken { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
