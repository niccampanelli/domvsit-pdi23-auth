using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.User;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public SignUpHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<SignUpOutput> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                input.Email = input.Email.ToLower();
                input.Password = _authenticationUseCase.EncryptPassword(input.Password);

                if (await _authenticationUseCase.VerifyEmailInUse(input.Email!) == true)
                {
                    var message = "O endereço de email informado já está em uso";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var createInput = new UserDto()
                {
                    Name = input.Name,
                    Email = input.Email,
                    Password = input.Password
                };

                var createResult = await _authenticationUseCase.CreateUser(createInput);

                var output = new SignUpOutput()
                {
                    Id = createResult.Id
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
