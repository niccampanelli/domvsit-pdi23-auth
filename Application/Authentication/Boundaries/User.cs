using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries
{
    public abstract class User
    {
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

        [SwaggerSchema(
            Title = "Senha",
            Description = "Senha do usuário",
            Format = "string"
            )]
        public string Password { get; set; }
    }
}
