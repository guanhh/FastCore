using FastCore.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SRM.HttpApi.Extensions;
using System;

namespace FastCore.TestBase
{
    public class FastCoreTest
    {
        private IServiceCollection _services;
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public IServiceProvider ServiceProvider => _serviceProvider;

        public FastCoreTest()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();

            _services = new ServiceCollection();

            //
            //基础功能注入
            _services.ConfigureFastCoreService(_configuration);

            //扩展服务注入
            _services.ConfigureExtensionService(_configuration);

            _serviceProvider = _services.BuildServiceProvider();
        }

        protected T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

    }
}
