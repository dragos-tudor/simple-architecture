
using Microsoft.AspNetCore.Builder;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  static string GetApiPathBase (WebApplication app) => app.Configuration["Kestrel:Endpoints:Http:Url"]!;
}