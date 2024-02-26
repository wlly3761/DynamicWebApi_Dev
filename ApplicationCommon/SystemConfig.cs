namespace ApplicationCommon
{
    public  class SystemConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static  string DBConnection { get; set; }
        public static string DBConnection2 { get; set; }
        public static JwtSetting JwtSetting { get; set; }
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
