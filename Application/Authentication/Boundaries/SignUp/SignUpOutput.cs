using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.SignUp
{
    public class SignUpOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do usuário criado",
            Format = "long"
            )]
        public long Id { get; set; }
    }
}
