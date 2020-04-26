using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using appoimntlastlq.Middlewares;
using appoimntlastlq.Models.DB;
using appointmentLast.Utiltes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace appoimntlastlq
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
         .MinimumLevel.Override("System", LogEventLevel.Fatal)
         //.ReadFrom.Configuration(configuration)
         .CreateLogger();


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            //services.AddDbContext<appointmentxContext>(op => op.UseSqlServer(
            //                      (Configuration["ConnectionStrings:Test"])));

            services.AddDbContext<appointmentxContext>(op => op.UseSqlServer(
                       (Configuration["ConnectionStrings:Server"])));
            //***********************************************************
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    int stateCode = context.Response.StatusCode;
                    ErrorMsg err =
               new ErrorMsg()
               {
                   Status = context.Response.StatusCode,
                   Title = CusStatusCode.Msg[stateCode],
                   TraceId = context.Request.Headers["TraceIdReq"],
                   Type = "ErrorMsg",
                   Message = "Sorry, this is not working properly. We now know about this mistake and are working to fix it."
               };
                    string error = JsonConvert.SerializeObject(err);
                    await context.Response.WriteAsync(error);
                });
            });
            app.UseMiddleware<ResponseLogMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
