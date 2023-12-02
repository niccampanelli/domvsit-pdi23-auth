using Swashbuckle.AspNetCore.Annotations;

namespace Application.Authentication.Boundaries.ResetPassword
{
    [SwaggerSchema(Required = new string[] { "OldPassword", "NewPassword" })]
    public class ResetPasswordInput
    {
        [SwaggerSchema(
            Title = "Login",
            Description = "Login cadastrado pelo usuário para autenticar",
            Format = "string"
            )]
        public string Login { get; set; }

        [SwaggerSchema(
            Title = "Senha atual",
            Description = "Senha atual do usuário",
            Format = "string"
            )]
        public string OldPassword { get; set; }

        [SwaggerSchema(
            Title = "Senha nova",
            Description = "Nova senha à ser definida",
            Format = "string"
            )]
        public string NewPassword { get; set; }
    }
}
