global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.Extensions.Logging;
global using Simple.Domain.Models;
global using Simple.Infrastructure.EmailServer;
global using Simple.Infrastructure.JobScheduler;
global using Simple.Infrastructure.MongoDb;
global using Simple.Infrastructure.Mediator;
global using Simple.Infrastructure.SqlServer;
global using Scheduling.JobScheduler;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.EmailServer.EmailServerFuncs;
global using static Simple.Infrastructure.JobScheduler.JobSchedulerFuncs;
global using static Simple.Infrastructure.Integrations.IntegrationsFuncs;
global using static Simple.Infrastructure.Mediator.MediatorFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Scheduling.JobScheduler.JobSchedulerFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.Infrastructure.Integrations;

public static partial class IntegrationsFuncs;