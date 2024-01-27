using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.RestoreUserData
{
    public class RestoreUserDataInput
    {
        [SwaggerSchema(
            Title = "Token de autorização",
            Description = "Token bearer JWT para autorizar o usuário",
            Format = "string"
            )]
        [FromHeader]
        public string Authorization { get; set; }
    }
}
