global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.Extensions.Logging;
global using Simple.Domain.Services;
global using Simple.Domain.Models;
global using Simple.Infrastructure.MongoDb;
global using Simple.Infrastructure.Mediator;
global using Simple.Infrastructure.Notifications;
global using Simple.Infrastructure.SqlServer;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Infrastructure.Mediator.MediatorFuncs;
global using static Simple.Infrastructure.Notifications.NotificationsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Web.Api.ApiFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

namespace Simple.Web.Api;

public static partial class ApiFuncs;