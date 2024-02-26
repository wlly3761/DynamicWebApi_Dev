using ApplicationCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;

namespace DynamicWebApi.BaseConfigSerivce.Jwt
{
    public static class JwtInit
    {
        public static void JwtRegesiterService(this IServiceCollection services, IConfiguration systemConfig)
        {
            string ss=systemConfig.GetValue<string>("JwtSetting:Audience");
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
            ValidAudience =  systemConfig.GetValue<string>("JwtSetting:Audience"),
            ValidIssuer = systemConfig.GetValue<string>("JwtSetting:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(systemConfig.GetValue<string>("JwtSetting:SecretKey")))
        };
    });
        }
    }
}
