using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.User
{
    [Table("refresh_token")]
    public class RefreshTokenEntity
    {
        [Key]
        public long Id { get; set; }

        public long? UserId { get; set; }

        public long? AttendantId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
