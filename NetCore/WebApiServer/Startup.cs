using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.TraceSource;

namespace WebApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCoreRepository");
            // 指定配置文件
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            init();
        }

        public IConfiguration Configuration { get; }
        public static ILoggerRepository repository { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<DynamicTransformer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swi = new ConsoleTraceListener();

            //string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            //string log = Path.Combine(logDir, $"log_{DateTime.Now:yyyyMMddHH}.log");
            //if (!Directory.Exists(logDir))
            //{
            //    Directory.CreateDirectory(logDir);
            //}

            //if (!File.Exists(log))
            //{
            //    using (File.Create(log))
            //    {
                    
            //    }
            //}
            //loggerFactory.AddProvider(new TraceSourceLoggerProvider(new SourceSwitch("WebApiServer", "Information"), new TextWriterTraceListener(log)));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.Use((conttext, next) => {
                var endpoint = conttext.GetEndpoint();//拿到终结点
                var routeData = conttext.Request.RouteValues;//拿到路由数据
                //routeData[""]
                //做些牛B的事
                return next();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapDynamicControllerRoute<DynamicTransformer>("{action}");
                endpoints.MapDynamicControllerRoute<DynamicTransformer>("{controller}/{action}");
                endpoints.MapDynamicControllerRoute<DynamicTransformer>("{subUrl1}/{controller}/{action}");
                endpoints.MapDynamicControllerRoute<DynamicTransformer>("{subUrl1}/{subUrl2}/{controller}/{action}");
                endpoints.MapDynamicControllerRoute<DynamicTransformer>("{subUrl1}/{subUrl2}/{subUrl3}/{controller}/{action}");
            });
        }
        void init()
        {
            if (WebApiServer.Controllers.TestController.dicRcMsg == null)
            {
                WebApiServer.Controllers.TestController.dicRcMsg = new Dictionary<string, string>();
                var sessions = Configuration.GetSection("WebApiServersPara").GetChildren();
                foreach (var session in sessions)
                {
                    WebApiServer.Controllers.TestController.dicRcMsg[session.Key] = session.Value;
                }
            }

        }
    }
}
