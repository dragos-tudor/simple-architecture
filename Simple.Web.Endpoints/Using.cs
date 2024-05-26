global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Simple.Infrastructure.SqlServer;
global using SqlFuncs = Storing.SqlServer.SqlServerFuncs;
global using Simple.Infrastructure.Mediator;
global using Simple.Shared.Models;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Infrastructure.Mediator.MediatorFuncs;
global using static Simple.Shared.Extensions.ExtensionsFuncs;
global using static Simple.Web.Endpoints.EndpointsFuncs;

namespace Simple.Web.Endpoints;

public static partial class EndpointsFuncs;