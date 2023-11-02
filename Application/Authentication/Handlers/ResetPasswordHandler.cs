﻿using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public ResetPasswordHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<ResetPasswordOutput> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                input.OldPassword = _authenticationUseCase.EncryptPassword(input.OldPassword);
                input.NewPassword = _authenticationUseCase.EncryptPassword(input.NewPassword);

                
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
