using Application.Authentication.Boundaries.RestoreUserData;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Authentication.Handlers
{
    public class RestoreUserDataHandler : IRequestHandler<RestoreUserDataCommand, RestoreUserDataOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public RestoreUserDataHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<RestoreUserDataOutput> Handle(RestoreUserDataCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;
                input.Authorization = Regex.Replace(input.Authorization, @"\bBearer\b", string.Empty).Trim();

                var userId = _authenticationUseCase.ExtractIdFromToken(input.Authorization);

                var user = await _authenticationUseCase.GetUserById(userId);

                var result = new RestoreUserDataOutput()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
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
