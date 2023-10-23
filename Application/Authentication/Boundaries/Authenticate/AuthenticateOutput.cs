using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.Authenticate
{
    public class AuthenticateOutput : User
    {
        [SwaggerSchema(
            Title = "Token",
            Description = "Bearer token para autenticar o usuário na aplicação",
            Format = "string"
            )]
        public string Token { get; set; }
    }
}
