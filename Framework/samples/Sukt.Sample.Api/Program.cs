using Sukt.Module.Core.Modules;
using Sukt.Sample.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication<SuktAppWebModule>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.InitializeApplication();

app.Run();
