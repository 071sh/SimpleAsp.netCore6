using Application.Interface.Setting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.JwtService
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            string issuer = string.Empty;
            string audience = string.Empty;
            string secretkey = string.Empty;

            using (var scope = serviceProvider.CreateScope())
            {
                var appSettingService = scope.ServiceProvider.GetService<IAppSettingService>();
                issuer = appSettingService.GetValue("Jwt.Issuer");
                audience = appSettingService.GetValue("Jwt.Audience");
                secretkey = appSettingService.GetValue("Jwt.SecretKey");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
                options.SaveToken = true; // HttpContext.GetTokenAsunc();
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //log 
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        //log
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
    }
}
