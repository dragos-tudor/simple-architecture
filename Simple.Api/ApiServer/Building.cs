
namespace Simple.Api;

partial class ApiFuncs
{
  internal static WebApplication BuildApiServer(string[] args, IConfiguration configuration, Action<WebApplicationBuilder> configBuilder)
  {
    var apierverBuilder = WebApplication.CreateBuilder(args);
    AddConfiguration(apierverBuilder.Configuration, configuration);
    RegisterServices(apierverBuilder.Services);
    configBuilder(apierverBuilder);

    var apiServer = apierverBuilder.Build();
    apiServer.UseExceptionHandler().UseRouting();
    return apiServer;
  }
}
