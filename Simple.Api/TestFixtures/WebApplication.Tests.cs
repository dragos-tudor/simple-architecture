
namespace Simple.Api;

partial class ApiTesting
{
  static string GetApiPathBase (WebApplication app) => app.Configuration["Kestrel:Endpoints:Http:Url"]!;
}