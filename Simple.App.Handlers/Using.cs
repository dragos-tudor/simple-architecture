global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Simple.Domain.Services;
global using Simple.Domain.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.App.Handlers;

public static partial class HandlersFuncs;