global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Scheduling.JobScheduler;
global using Simple.Infrastructure.JobScheduler;
global using Simple.Infrastructure.MongoDb;
global using Simple.Infrastructure.SqlServer;
global using static Scheduling.JobScheduler.JobSchedulerFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Worker.Jobs.JobsFuncs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Simple.Worker.Tests")]

namespace Simple.Worker;

public static partial class WorkerFuncs;