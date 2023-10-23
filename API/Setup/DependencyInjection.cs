﻿using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Commands;
using Application.Authentication.Handlers;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateOutput>, AuthenticateHandler>();
            services.AddTransient<IRequestHandler<ResetPasswordCommand, ResetPasswordOutput>, ResetPasswordHandler>();
            services.AddTransient<IRequestHandler<SignUpCommand, SignUpOutput>, SignUpHandler>();

            services.AddScoped<IAuthenticationUseCase, AuthenticationUseCase>();
        }
    }
}
