using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Boundaries.RevalidateToken;
using Application.Authentication.Commands;
using Application.Authentication.Handlers;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Repository;
using Infrastructure.Repository;
using MediatR;
using Application.Authentication.Boundaries.RestoreUserData;
using Application.Authentication.Boundaries.GenerateTokenForAttendant;
using Application.Authentication.Boundaries.ExtractIdFromToken;

namespace API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddTransient<IRequestHandler<AuthenticateCommand, AuthenticateOutput>, AuthenticateHandler>();
            services.AddTransient<IRequestHandler<ExtractIdFromTokenCommand, ExtractIdFromTokenOutput>, ExtractIdFromTokenHandler>();
            services.AddTransient<IRequestHandler<GenerateTokenForAttendantCommand, GenerateTokenForAttendantOutput>, GenerateTokenForAttendantHandler>();
            services.AddTransient<IRequestHandler<ResetPasswordCommand, ResetPasswordOutput>, ResetPasswordHandler>();
            services.AddTransient<IRequestHandler<RestoreUserDataCommand, RestoreUserDataOutput>, RestoreUserDataHandler>();
            services.AddTransient<IRequestHandler<RevalidateTokenCommand, RevalidateTokenOutput>, RevalidateTokenHandler>();
            services.AddTransient<IRequestHandler<SignUpCommand, SignUpOutput>, SignUpHandler>();

            services.AddScoped<IAuthenticationUseCase, AuthenticationUseCase>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
