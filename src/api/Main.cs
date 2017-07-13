using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace api
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env) {

        }

        // This method is called by the runtime. Use this method to add services
        // to the container
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });
        }

        // This method is called by the runtime. Use this method to configure
        // the HTTP request pipeline
        public void Configure(IApplicationBuilder app) {
            app.UseMvc();
        }
    }
}
