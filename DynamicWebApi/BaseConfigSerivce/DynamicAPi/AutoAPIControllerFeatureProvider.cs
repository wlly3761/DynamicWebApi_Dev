using ApplicationCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace DynamicWebApi.BaseConfigSerivce.DynamicAPi
{
    public class AutoAPIControllerFeatureProvider : ControllerFeatureProvider
    {
        //protected override bool IsController(TypeInfo typeInfo)
        //{
        //    //判断是否继承了指定的接口
        //    if (typeof(IAutoAPIService).IsAssignableFrom(typeInfo))
        //    {
        //        if (!typeInfo.IsInterface &&
        //            !typeInfo.IsAbstract &&
        //            !typeInfo.IsGenericType &&
        //            typeInfo.IsPublic)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        protected override bool IsController(TypeInfo typeInfo)
        {
            //使用注解的方式进行识别
            var hasAnnotation = Attribute.IsDefined(typeInfo, typeof(DynamicApiInterfaceAttribute));
            //#使用接口的方式进行识别#
            //var hasAnnotation = typeof(DynamicApiInterfaceAttribute).GetCustomAttributes(typeof(DynamicApiInterfaceAttribute), false).Any();
            //判断是否使用了指定注解
            if (hasAnnotation)
            {
                if (!typeInfo.IsInterface &&
                    !typeInfo.IsAbstract &&
                    !typeInfo.IsGenericType &&
                    typeInfo.IsPublic)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
