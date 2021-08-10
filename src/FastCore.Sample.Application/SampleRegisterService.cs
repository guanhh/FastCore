using FastCore.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FastCore.Sample.Application
{
    public class SampleRegisterService : IRegisterService
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            ServiceRegister.RegServices(services, typeof(SampleRegisterService).Namespace);
        }
    }
}
