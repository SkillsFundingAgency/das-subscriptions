using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Esfa.Recruit.Subscriptions.Web.Configuration;

namespace Esfa.Recruit.Subscriptions.Web
{
    public partial class Startup
    {
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration config, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _configuration = config;
            _hostingEnvironment = env;
            _loggerFactory = loggerFactory;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Routing has to come before adding Mvc
            services.AddRouting(opt =>
            {
                opt.AppendTrailingSlash = true;
            });

            services.AddMvcService(_hostingEnvironment, _loggerFactory);
            services.AddApplicationInsightsTelemetry(_configuration);
        }
    }
}
