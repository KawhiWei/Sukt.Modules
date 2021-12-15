using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sukt.Module.Core.Middleware;
using Sukt.WebSocketServer.MvcHandler;
using System;
using System.Collections.Generic;

namespace Sukt.WebSocketServer.Demo
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sukt.WebSocketServer.Demo", Version = "v1" });
            });
            services.AddSuktWebSocketConfigRouterEndpoint(x =>
            {

                x.WebSocketChannels = new Dictionary<string, WebSocketRouteOption.WebSocketChannelHandler>()
                {
                    { "/im",new MvcChannelHandler(4*1024).ConnectionEntry}
                };
                x.ApplicationServiceCollection = services;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sukt.WebSocketServer.Demo v1"));
            }
            app.UseErrorHandling();

            #region WebSocket
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(15),//服务的主动向客户端发起心跳检测时间
                ReceiveBufferSize = 4 * 1024//数据缓冲区
            };
            app.UseWebSockets(webSocketOptions);
            app.UseSuktWebSocketServer(app.ApplicationServices);
            #endregion
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
