using Microsoft.OpenApi.Models;

namespace DynamicWebApi.BaseConfigSerivce.Swagger
{
    /// <summary>
    /// Swagger扩展类
    /// </summary>
    public static class SwaggerExtend
    {
        public static IServiceCollection AddSwaggerGenExtend(this IServiceCollection serviceCollection)
        {
            string ApiName = "Webapi.Core";
            return serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{ApiName} 接口文档——Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                });
                c.OrderActionsBy(o => o.RelativePath);
                #region 启用swagger验证功能
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });
                #endregion
            });
        }
    }
}
