using Application.Authentication.Boundaries.RevalidateToken;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.User;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Authentication.Handlers
{
    public class RevalidateTokenHandler : IRequestHandler<RevalidateTokenCommand, RevalidateTokenOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public RevalidateTokenHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<RevalidateTokenOutput> Handle(RevalidateTokenCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Authorization = Regex.Replace(input.Authorization, @"\bBearer\b", string.Empty).Trim();
                
                if (!await _authenticationUseCase.ValidateToken(input.Authorization))
                {
                    var message = "Token de acesso inválido";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                if (!await _authenticationUseCase.ValidateRefreshToken(input.RefreshToken))
                {
                    var message = "Token de revalidação inválido";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var userId = _authenticationUseCase.ExtractIdFromToken(input.Authorization);

                var isRefreshTokenRegisteredInput = new RefreshTokenDto()
                {
                    UserId = userId,
                    Value = input.RefreshToken
                };

                if (!await _authenticationUseCase.IsRefreshTokenRegistered(isRefreshTokenRegisteredInput))
                {
                    var message = "Token de revalidação não encontrado";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var newToken = _authenticationUseCase.GenerateToken(userId);

                if (newToken == null)
                {
                    var message = "Não foi possível gerar um novo token";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var result = new RevalidateTokenOutput()
                {
                    Token = newToken
                };

                return result;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
