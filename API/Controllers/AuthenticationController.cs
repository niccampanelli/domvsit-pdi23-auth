﻿using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Boundaries.ValidateAuthentication;
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
    [AllowAnonymous]
    [SwaggerTag("Rotas de controle de acesso da aplicação")]
    public class AuthenticationController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthenticationController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("[action]")]
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
        [SwaggerOperation(Summary = "Validar autenticação", Description = "Valida os tokens informados na requisição e devolve novos dados caso necessário.")]
        [SwaggerResponse(200, Description = "Sucesso", Type = typeof(ValidateAuthenticationOutput))]
        [SwaggerResponse(400, Description = "Erros 400", Type = typeof(List<string>))]
        [SwaggerResponse(500, Description = "Erros 500", Type = typeof(List<string>))]
        public async Task<IActionResult> ValidateAuthentication([FromHeader] ValidateAuthenticationInput input)
        {
            var command = new ValidateAuthenticationCommand(input);
            var result = await _mediatorHandler.SendCommand<ValidateAuthenticationCommand, ValidateAuthenticationOutput>(command);

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
