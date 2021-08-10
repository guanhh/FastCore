using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.Base
{
    public interface IRegisterService
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
