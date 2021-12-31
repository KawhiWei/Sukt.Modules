using Serilog;
using Serilog.Events;
using Sukt.AuthServer.Demo;
using Sukt.AuthServer.Demo.Startups;
using Sukt.Module.Core.Middleware;
using Sukt.Module.Core.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((webHost, logconfiguration) =>{
        //得到配置文件
        var serilog = webHost.Configuration.GetSection("Serilog");
        //最小级别
        var minimumLevel = serilog["MinimumLevel:Default"];
        //日志事件级别
        var logEventLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), minimumLevel);

        logconfiguration.ReadFrom.Configuration(webHost.Configuration.GetSection("Serilog")).Enrich.FromLogContext().WriteTo.Console(logEventLevel);

        logconfiguration.WriteTo.Map(le => MapData(le),(key, log) => log.Async(o => o.File(Path.Combine("logs", @$"{key.time:yyyy-MM-dd}\{key.level.ToString().ToLower()}.txt"), logEventLevel)));

        (DateTime time, LogEventLevel level) MapData(LogEvent logEvent)
        {

            return (new DateTime(logEvent.Timestamp.Year, logEvent.Timestamp.Month, logEvent.Timestamp.Day, logEvent.Timestamp.Hour, logEvent.Timestamp.Minute, logEvent.Timestamp.Second), logEvent.Level);
        }

    })
    .ConfigureLogging((hostingContext, builder) =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        builder.AddConsole();
        builder.AddDebug();
    });


// Add services to the container.
builder.Services.AddApplication<SuktAppWebModule>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorHandling();
app.UseAuthorization();
app.MapControllers();
app.InitializeApplication();
app.Run();
