using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace FastCore.Base
{
    public class ServiceRegister
    {

        public static void RegServices(IServiceCollection services, string nmspace)
        {
            var assembly = Assembly.Load(nmspace);

            if (assembly == null)
                return;

            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!typeof(ISingleService).GetTypeInfo().IsAssignableFrom(type))
                {
                    continue;
                }

                Type[] interfaces = type.GetInterfaces();

                foreach (var item in interfaces)
                {
                    if (item.Name[1..] != type.Name)
                        continue;

                    services.AddScoped(item, type);
                }
            }

        }

    }
}
