using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.ExtractIdFromToken
{
    public class ExtractIdFromTokenInput
    {
        [SwaggerSchema(
            Title = "Token de autorização",
            Description = "Token bearer JWT para extrair o id",
            Format = "string"
            )]
        [FromHeader]
        public string Authorization { get; set; }
    }
}
