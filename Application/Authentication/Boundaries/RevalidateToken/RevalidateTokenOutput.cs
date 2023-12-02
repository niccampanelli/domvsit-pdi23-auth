using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.RevalidateToken
{
    public class RevalidateTokenOutput
    {
        [SwaggerSchema(
            Title = "Token",
            Description = "Bearer token para autenticar o usuário na aplicação",
            Format = "string"
            )]
        public string Token { get; set; }
    }
}
