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
        private IOptionsMonitor<SystemConfig> _positionOptions;

        public Options(IOptionsMonitor<SystemConfig> optionsMonitor)
        {
            _positionOptions= optionsMonitor;
        }

        public object GetConfigure()
        {
            
            return _positionOptions.CurrentValue;
        }
    }
}
