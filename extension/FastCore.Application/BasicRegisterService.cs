using FastCore.Abstract;
using FastCore.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.Application
{
    public class BasicRegisterService : IRegisterService
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentUser, CurrentUser>();

            ServiceRegister.RegServices(services, typeof(BasicRegisterService).Namespace);
        }


    }
}
