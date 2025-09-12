global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Simple.Shared.Events;
global using Simple.Shared.Models;
global using Simple.Infrastructure.MailServer;
global using Simple.Infrastructure.JobScheduler;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Messaging.Handlers.HandlersFuncs;
global using static Simple.Infrastructure.JobScheduler.JobSchedulerFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.Worker.Jobs;

public static partial class JobsFuncs;