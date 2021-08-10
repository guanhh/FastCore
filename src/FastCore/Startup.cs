using FastCore.Extension;
using FastCore.Model.Result;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SRM.HttpApi.Extensions;
using System.Text.Json;

namespace FastCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //基础功能注入
            services.ConfigureFastCoreService(Configuration);

            //扩展服务注入
            services.ConfigureExtensionService(Configuration);

            services.AddRouting(options => options.LowercaseUrls = true);
            //
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FastCore", Version = "v1" });
            });

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastCore v1"));
            }

            app.UseExceptionHandler(HandleError);

            app.UseFastCore();
        }


        private void HandleError(IApplicationBuilder builder)
        {
            builder.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new ResultMsg<string>()
                        {
                            code = (int)StatusCode.Error,
                            message = error.Error.Message
                        }));
                }
            });
        }

    }
}
