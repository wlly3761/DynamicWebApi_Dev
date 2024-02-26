using ApplicationCommon;
using DynamicWebApi.BaseConfigSerivce;
using DynamicWebApi.BaseConfigSerivce.DynamicAPi;
using DynamicWebApi.BaseConfigSerivce.Filter;
using DynamicWebApi.BaseConfigSerivce.Jwt;
using DynamicWebApi.BaseConfigSerivce.SqlSugar;
using DynamicWebApi.BaseConfigSerivce.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Bind(new SystemConfig());
//���ض�̬API
builder.Services.AddControllers().AddDynamicWebApi();
//����swaggerDoc�ĵ�����
builder.Services.AddSwaggerGenExtend();
//�Զ�ע�����
builder.Services.AutoRegistryService();
//ע��SqlSugar����
builder.Services.AddSqlsugarSetup();
//ע��JWT��֤����
builder.Services.JwtRegesiterService();
//��������API����
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