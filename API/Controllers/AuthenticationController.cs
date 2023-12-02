using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Boundaries.RevalidateToken;
using Application.Authentication.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [SwaggerTag("Rotas de controle de acesso da aplicação")]
    public class AuthenticationController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthenticationController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Autenticar usuário", Description = "Autentica o usuário e devolve o token para acessar a aplicação.")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(AuthenticateOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateInput input)
        {
            var command = new AuthenticateCommand(input);
            var result = await _mediatorHandler.SendCommand<AuthenticateCommand, AuthenticateOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Redefinir senha", Description = "Define uma nova senha para o usuário se a senha atual informada estiver correta.")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(ResetPasswordOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordInput input)
        {
            var command = new ResetPasswordCommand(input);
            var result = await _mediatorHandler.SendCommand<ResetPasswordCommand, ResetPasswordOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Cadastrar usuário", Description = "Cadastra um novo usuário na aplicação.")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(SignUpOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> SignUp([FromBody] SignUpInput input)
        {
            var command = new SignUpCommand(input);
            var result = await _mediatorHandler.SendCommand<SignUpCommand, SignUpOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Revalidar token", Description = "Utiliza o refresh token para gerar um novo token de acesso válido")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(RevalidateTokenOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> RevalidateToken(
            [FromHeader(Name = "Authorization")] string authorization,
            [FromHeader(Name = "RefreshToken")] string refreshToken)
        {
            var input = new RevalidateTokenInput()
            {
                Authorization = authorization,
                RefreshToken = refreshToken
            };

            var command = new RevalidateTokenCommand(input);
            var result = await _mediatorHandler.SendCommand<RevalidateTokenCommand, RevalidateTokenOutput>(command);

            if (IsValidOperation())
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(GetMessages());
            }
        }
    }
}
