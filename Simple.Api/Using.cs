global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Simple.Shared.Models;
global using Simple.Infrastructure.MongoDb;
global using Simple.Infrastructure.SqlServer;
global using static Storing.SqlServer.SqlServerFuncs;
global using static Simple.Infrastructure.MessageQueue.MessageQueueFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Api.Endpoints.EndpointsFuncs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Simple.Api.Tests")]

namespace Simple.Api;

public static partial class ApiFuncs;