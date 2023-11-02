using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Setup
{
    public static class AuthenticationConfig
    {
        public static void AddJwtAuthentication(this IServiceCollection services, string secret)
        {
            var skey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(skey.Key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
