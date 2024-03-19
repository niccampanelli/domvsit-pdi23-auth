using Application.Authentication.Boundaries.GenerateTokenForAttendant;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.User;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class GenerateTokenForAttendantHandler : IRequestHandler<GenerateTokenForAttendantCommand, GenerateTokenForAttendantOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public GenerateTokenForAttendantHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<GenerateTokenForAttendantOutput> Handle(GenerateTokenForAttendantCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                var token = _authenticationUseCase.GenerateToken(input.AttendantId, TokenUserTypeEnum.Attendant);

                var refreshToken = _authenticationUseCase.GenerateRefreshToken();

                await _authenticationUseCase.RemoveRegisteredAttendantRefreshTokens(input.AttendantId);

                var registerRefreshTokenInput = new RefreshTokenDto()
                {
                    AttendantId = input.AttendantId,
                    Value = refreshToken
                };

                await _authenticationUseCase.RegisterRefreshTokenSession(registerRefreshTokenInput);

                var output = new GenerateTokenForAttendantOutput()
                {
                    Token = token,
                    RefreshToken = refreshToken
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
