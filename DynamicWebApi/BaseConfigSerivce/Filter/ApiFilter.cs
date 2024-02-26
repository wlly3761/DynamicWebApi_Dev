using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Dm.net.buffer.ByteArrayBuffer;

namespace DynamicWebApi.BaseConfigSerivce.Filter
{
    public class ApiFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //context.Result = new JsonResult(new { code=404, msg="非法请求" });
            //return;
        }
    }
}
