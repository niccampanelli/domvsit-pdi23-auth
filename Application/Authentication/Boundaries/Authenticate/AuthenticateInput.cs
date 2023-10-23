using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.Authenticate
{
    [SwaggerSchema(Required = new string[] { "Login", "Password" })]
    public class AuthenticateInput
    {
        [SwaggerSchema(
            Title = "Login",
            Description = "Login cadastrado pelo usuário para cadastrar",
            Format = "string"
            )]
        public string Login { get; set; }

        [SwaggerSchema(
            Title = "Senha",
            Description = "Senha associada ao usuário",
            Format = "string"
            )]
        public string Password { get; set; }
    }
}
