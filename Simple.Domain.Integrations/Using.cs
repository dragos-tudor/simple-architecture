global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.Extensions.Logging;
global using Simple.Domain.Models;
global using Simple.Domain.Services;
global using Simple.Infrastructure.EmailServer;
global using Simple.Infrastructure.Mediator;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.EmailServer.EmailServerFuncs;
global using static Simple.Infrastructure.Mediator.MediatorFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.Domain.Integrations;

public static partial class IntegrationsFuncs;