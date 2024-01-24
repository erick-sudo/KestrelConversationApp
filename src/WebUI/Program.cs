using AdminPortal.Middlewares;
using Infrastructure.ExtensionMethods;
using Serilog;
using WebUI.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Configuration.AddAppSettings();

builder.Services.AddInfrastructure(builder);

builder.Host.UseSerilog((_, config) => config
    .ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

app.UseRouting();

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.EnsureDatabaseCreated();

app.Run();
