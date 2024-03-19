using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.GenerateTokenForAttendant
{
    public class GenerateTokenForAttendantOutput
    {
        [SwaggerSchema(
            Title = "Token",
            Description = "Bearer token para autenticar o usuário na aplicação",
            Format = "string"
            )]
        public string Token { get; set; }

        [SwaggerSchema(
            Title = "RefreshToken",
            Description = "Token para revalidar o acesso do usuário na aplicação",
            Format = "string"
            )]
        public string RefreshToken { get; set; }
    }
}
