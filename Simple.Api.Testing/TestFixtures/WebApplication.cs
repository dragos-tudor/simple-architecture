
using Microsoft.AspNetCore.Builder;

namespace Simple.Api.Testing;

partial class TestingFuncs
{
  static string GetApiPathBase (WebApplication app) => app.Configuration["Kestrel:Endpoints:Http:Url"]!;
}