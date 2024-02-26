using ApplicationCommon;
using DynamicWebApi.BaseConfigSerivce;
using DynamicWebApi.BaseConfigSerivce.DynamicAPi;
using DynamicWebApi.BaseConfigSerivce.Filter;
using DynamicWebApi.BaseConfigSerivce.Jwt;
using DynamicWebApi.BaseConfigSerivce.SqlSugar;
using DynamicWebApi.BaseConfigSerivce.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Bind(new SystemConfig());
//加载动态API
builder.Services.AddControllers().AddDynamicWebApi();
//加载swaggerDoc文档修饰
builder.Services.AddSwaggerGenExtend();
//自动注入服务
builder.Services.AutoRegistryService();
//注入SqlSugar服务
builder.Services.AddSqlsugarSetup();
//注册JWT验证服务
builder.Services.JwtRegesiterService();
//拦截所有API请求
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ApiFilter>();
});
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration="localhost:6379";
    options.InstanceName="DemoInstance";
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout=TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly=true;
});
builder.Services.AddDataProtection(configure =>
{
    configure.ApplicationDiscriminator="singleLogin";
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Core V1");
    c.RoutePrefix="ApiDoc";
});
app.UseSession();

app.Run();