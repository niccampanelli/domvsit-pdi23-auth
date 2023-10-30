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

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do usuário",
            Format = "string"
            )]
        public string Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Endereço de email do usuário",
            Format = "string"
            )]
        public string Email { get; set; }
    }
}
