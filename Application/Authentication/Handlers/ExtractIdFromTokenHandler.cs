using Application.Authentication.Boundaries.ExtractIdFromToken;
using Application.Authentication.Boundaries.GenerateTokenForAttendant;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Authentication.Handlers
{
    public class ExtractIdFromTokenHandler : IRequestHandler<ExtractIdFromTokenCommand, ExtractIdFromTokenOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public ExtractIdFromTokenHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<ExtractIdFromTokenOutput> Handle(ExtractIdFromTokenCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Authorization = Regex.Replace(input.Authorization, @"\bBearer\b", string.Empty).Trim();

                var extractResult = _authenticationUseCase.ExtractIdFromToken(input.Authorization);

                var output = new ExtractIdFromTokenOutput()
                {
                    UserId = extractResult.UserId,
                    AttendantId = extractResult.AttendantId
                };

                return output;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
