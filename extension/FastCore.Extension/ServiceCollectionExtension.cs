using FastCore.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FastCore.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddExtensionService(this IServiceCollection services, IConfiguration configuration)
        {
            //动态注入 IRegisterService
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(path, "*.Application.dll", SearchOption.TopDirectoryOnly).ToList();

            foreach (var file in files)
            {
                var ass = Assembly.LoadFrom(file);
                var types = ass.GetTypes();

                foreach (var t in types)
                {
                    if (typeof(IRegisterService).IsAssignableFrom(t))
                    {
                        var regService = (IRegisterService)Activator.CreateInstance(t);
                        regService.Register(services, configuration);
                    }
                }

            }
        }

    }
}
