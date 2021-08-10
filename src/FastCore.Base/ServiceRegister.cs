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

                if (typeof(ISingleService).GetTypeInfo().IsAssignableFrom(type))
                {
                    RegService(services, type, (s, t1, t2) => s.AddSingleton(t1, t2));
                }

                if (typeof(IScopeService).GetTypeInfo().IsAssignableFrom(type))
                {
                    RegService(services, type, (s, t1, t2) => s.AddScoped(t1, t2));
                }

                if (typeof(ITransientService).GetTypeInfo().IsAssignableFrom(type))
                {
                    RegService(services, type, (s, t1, t2) => s.AddTransient(t1, t2));
                }

            }

        }

        private static void RegService(IServiceCollection services, Type type, Action<IServiceCollection, Type, Type> action)
        {
            Type[] interfaces = type.GetInterfaces();

            foreach (var item in interfaces)
            {
                if (item.Name[1..] != type.Name)
                    continue;

                action(services, item, type);
            }
        }

    }
}
