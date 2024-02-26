namespace ApplicationCommon
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public class ServiceRegistryAttribute:Attribute
    {
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public string ServicelLifeCycle { get; set; }
    }
}
