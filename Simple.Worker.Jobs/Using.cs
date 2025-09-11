global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Simple.Shared.Models;
global using Simple.Infrastructure.JobScheduler;
global using Simple.Domain.Models;
global using Simple.Domain.Services;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.JobScheduler.JobSchedulerFuncs;
global using static Simple.Infrastructure.MailServer.MailServerFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.Worker.Jobs;

public static partial class JobsFuncs;