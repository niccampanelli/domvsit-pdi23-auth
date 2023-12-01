using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.RevalidateToken
{
    public class RevalidateTokenInput
    {
        [SwaggerSchema(
            Title = "Token de autorização",
            Description = "Token bearer JWT para autorizar o usuário",
            Format = "string"
            )]
        [FromHeader]
        public string Authorization { get; set; }

        [SwaggerSchema(
            Title = "Token de revalidação",
            Description = "Token JWT para revalidar o token de autorização",
            Format = "string"
            )]
        [FromHeader]
        public string RefreshToken { get; set; }
    }
}
