using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.GenerateTokenForAttendant
{
    [SwaggerSchema(Required = new string[] { "AttendantId" })]
    public class GenerateTokenForAttendantInput
    {
        [SwaggerSchema(
            Title = "Id do participante",
            Description = "Id do participante ao qual se deve gerar os tokens",
            Format = "long"
            )]
        public long AttendantId { get; set; }
    }
}
