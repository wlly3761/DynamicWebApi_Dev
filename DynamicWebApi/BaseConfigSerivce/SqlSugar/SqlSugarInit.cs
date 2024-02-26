using ApplicationCommon;
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
            services.AddSqlSugar(new IocConfig
            {
                ConfigId="1",
                DbType=IocDbType.SqlServer,//必填, 数据库类型
                ConnectionString=configuration.GetValue<string>("DBConnection"),//必填, 数据库连接字符串
                IsAutoCloseConnection = true//默认false, 是否自动关闭数据库连接, 设置为true无需使用using或者Close操作
            });
            services.AddSqlSugar(new IocConfig()
            {
                ConfigId="2",
                ConnectionString = configuration.GetValue<string>("DBConnection2"),
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
            services.ConfigurationSugar(db =>
            {
                //db.GetConnection("1").Aop.OnLogExecuting = (sql, p) =>
                //{
                //    Console.WriteLine(1+sql);
                //};
            });
        }
    }

}
