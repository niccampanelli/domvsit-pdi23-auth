using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User
{
    [Table("refresh_token")]
    public class RefreshTokenEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        public UserEntity? User { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
