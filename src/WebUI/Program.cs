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

// builder.WebHost.UseKestrel(options =>
// {
//     // Paths to the certificate files
//     string cetificatePath = "/https/conversationapp.crt";
//     string cetificateKeyPath = "/https/conversationapp.key";

//     // Configure Kestrel to use the provided certificates
//     options.ListenAnyIP(8080); // HTTP
//     options.ListenAnyIP(8081, listenOptions =>
//     {
//         // HTTPS
//         listenOptions.UseHttps(cetificatePath, cetificateKeyPath);
//     });
// });

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
