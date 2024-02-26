namespace ApplicationCommon
{
    public class SystemConfig
    {
        public const string ConfigName = "SystemConfig";
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public  string DBConnection { get;  set; }
        public  string DBConnection2 { get;  set; }
        public  JwtSetting JwtSetting { get;  set; }
    }
    public class JwtSetting
    {
        /// <summary>
        /// 证书颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        ///  角色
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密字符串
        /// </summary>
        public string SecretKey { get; set; }

    }
}
