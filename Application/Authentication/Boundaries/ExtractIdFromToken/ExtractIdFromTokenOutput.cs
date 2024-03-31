using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.ExtractIdFromToken
{
    public class ExtractIdFromTokenOutput
    {
        [SwaggerSchema(
            Title = "Id de usuário",
            Description = "Id de usuário extraído do token",
            Format = "long"
            )]
        public long? UserId { get; set; }

        [SwaggerSchema(
            Title = "Id de participante",
            Description = "Id de participante extraído do token",
            Format = "long"
            )]
        public long? AttendantId { get; set; }
    }
}
