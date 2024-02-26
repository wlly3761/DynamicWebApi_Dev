using ApplicationCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Application.Session
{
    [DynamicApiInterface]
    [ServiceRegistry(ServicelLifeCycle = "Scoped")]
    public class Session : ISeesion
    {
        private readonly HttpContext _context;
        private readonly IDistributedCache _cache;
        public Session(IHttpContextAccessor contextAccessor, IDistributedCache cache)
        {
            _context=contextAccessor.HttpContext;
            _cache=cache;
        }
        [HttpGet]
        public string GetSessionByKey(string key,string val)
        {
            string values=_context.Session.GetString(key);
            if (string.IsNullOrEmpty(values))
            {
                _context.Session.SetString(key, val);
            }
            return values;
        }
        [HttpGet]
        public  string GetCacheByKey(string key, string val)
        {
            var cacheEntryOptions = new DistributedCacheEntryOptions()
          .SetAbsoluteExpiration(TimeSpan.FromMinutes(1)); // 设置绝对过期时间（注意，Program.cs类中的AddSession设置的过期时间会影响覆盖这设置的时间）
            string values = _cache.GetString(key);
            if (string.IsNullOrEmpty(values))
            {
                _cache.SetString(key, val, cacheEntryOptions);
            }
            return  values;
        }
    }
}
