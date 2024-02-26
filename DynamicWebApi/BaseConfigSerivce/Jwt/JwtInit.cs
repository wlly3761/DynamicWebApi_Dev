using ApplicationCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DynamicWebApi.BaseConfigSerivce.Jwt
{
    public static class JwtInit
    {
        public static void JwtRegesiterService(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(p =>
    {
        p.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            ValidateIssuerSigningKey = true,
            ValidAudience = SystemConfig.JwtSetting.Audience,
            ValidIssuer = SystemConfig.JwtSetting.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConfig.JwtSetting.SecretKey))
        };
    });
        }
    }
}
