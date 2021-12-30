using Sukt.AuthServer.Demo.Startups;
using Sukt.Module.Core.Middleware;
using Sukt.Module.Core.Modules;

var builder = WebApplication.CreateBuilder(args);

// builder.Host.ConfigureWebHostDefaults(webbuilder =>
//{
//    webbuilder.UseStartup<Startup>();
//});

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
