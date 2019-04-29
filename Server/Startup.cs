using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromocodeService.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace PromocodeService
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
            var config = new ServerConfig();
            Configuration.Bind(config);

            //var redisHost = Configuration.GetValue<string>("Redis:Host");
            //var redisPort = Configuration.GetValue<int>("Redis:Port");
            //var redisIp = System.Net.Dns.GetHostEntryAsync(redisHost).Result.AddressList.Last();
            //var redis = ConnectionMultiplexer.Connect($"{redisIp}:{redisPort}");

            //services.AddDataProtection();
            //services.Configure<KeyManagementOptions>(o =>
            //{
            //    o.XmlRepository = new RedisXmlRepository(() => redis.GetDatabase(), "DataProtection-Keys");
            //});

            var promocodeContext = new PromocodeContext(config.MongoDB);
            var repo = new PromocodeRepository(promocodeContext);

            services.AddSingleton<IPromocodeRepository>(repo);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //add api visualization
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Promocode Service API",
                    Version = "v1",
                    Description = "Promocode Service API tutorial using MongoDB",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Promocode Service API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
