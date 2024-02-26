using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicWebApi.BaseConfigSerivce.DynamicAPi
{
    public static class AutoAPIEx
    {
        public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureApplicationPartManager(applicationPartManager =>
            {
                applicationPartManager.FeatureProviders.Add(new AutoAPIControllerFeatureProvider());
            });

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new AutoAPIApplicationModelConvention());
            });

            return builder;
        }

        public static IMvcCoreBuilder AddDynamicWebApi(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureApplicationPartManager(applicationPartManager =>
            {
                applicationPartManager.FeatureProviders.Add(new AutoAPIControllerFeatureProvider());
            });

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new AutoAPIApplicationModelConvention());
            });

            return builder;
        }
    }
}
