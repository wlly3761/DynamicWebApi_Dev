using ApplicationCommon;

namespace DynamicWebApi.BaseConfigSerivce
{
    public static class AutoInjectService
    {
        public static IServiceCollection AutoRegistryService(this IServiceCollection serviceCollection)
        {
            var types = AssemblyHelper.GetTypesByAssembly("Application").ToArray();
            foreach (var serviceType in types)
            {
                if (Attribute.IsDefined(serviceType, typeof(ServiceRegistryAttribute)))
                {
                    var serviceRegistryAttribute = (ServiceRegistryAttribute)serviceType.GetCustomAttributes(typeof(ServiceRegistryAttribute), false).FirstOrDefault();

                    var interfaces = serviceType.GetInterfaces();
                    switch (serviceRegistryAttribute.ServicelLifeCycle)
                    {
                        case "Singleton":
                            serviceCollection.AddSingleton(interfaces.FirstOrDefault(), serviceType);
                            break;
                        case "Scoped":
                            serviceCollection.AddScoped(interfaces.FirstOrDefault(), serviceType);
                            break;
                        case "Transient":
                            serviceCollection.AddTransient(interfaces.FirstOrDefault(), serviceType);
                            break;
                    }

                }
            }
            return serviceCollection;
        }
    }
}