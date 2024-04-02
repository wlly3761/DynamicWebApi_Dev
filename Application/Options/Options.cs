using ApplicationCommon;
using ApplicationCommon.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Options
{
    [DynamicApiInterface]
    [ServiceRegistry(ServicelLifeCycle = "Scoped")]
    public class Options : Ioptions
    {
        private readonly IConfiguration _configuration;
        private IOptionsMonitor<SystemConfig> _positionOptions;

        public Options(IConfiguration configuration,IOptionsMonitor<SystemConfig> optionsMonitor)
        {
            _configuration=configuration;
            _positionOptions = optionsMonitor;
        }

        public object GetConfigure()
        {
            return _positionOptions.CurrentValue;
        }

        public string GetConfigValue(string key)
        {
           string str= _configuration.GetValue<string>(key);
            return str;
        }
    }
}
