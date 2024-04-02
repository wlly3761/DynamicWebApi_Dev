using ApplicationCommon;
using SqlSugar;
using SqlSugar.IOC;

namespace DynamicWebApi.BaseConfigSerivce.SqlSugar
{
    /// <summary>
    /// SqlSugar 依赖注入
    /// </summary>
    public static class SqlSugarInit
    {

        public static void AddSqlsugarSetup(this IServiceCollection services,IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddScoped<ISqlSugarClient>(o =>
            {
                var client = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = configuration.GetValue<string>("DBConnection"),
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    ConfigId = "1"
                });
                return client;
            });
            services.AddScoped<ISqlSugarClient>(o =>
            {
                var client = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = configuration.GetValue<string>("DBConnection2"),
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    ConfigId = "2"
                });
                return client;
            });
            services.ConfigurationSugar(db =>
            {
                db.GetConnection("1").Aop.OnLogExecuting = (sql, p) =>
                {
                    Console.WriteLine(1+sql);
                };
            });
        }
    }

}
