
namespace Simple.Api;

partial class ApiFuncs
{
  internal static WebApplication BuildApplication (string[] args, IConfiguration configuration, Action<WebApplicationBuilder> configBuilder)
  {
    var builder = WebApplication.CreateBuilder(args);
    RegisterLogging(builder.Services, IntegrateSerilog(configuration));
    RegisterServices(builder.Services);
    AddConfiguration(builder.Configuration, configuration);
    configBuilder(builder);

    var app = builder.Build();
    app.UseExceptionHandler().UseRouting();
    return app;
  }
}
