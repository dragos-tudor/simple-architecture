global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using Simple.App.Services;
global using Simple.Domain.Models;
global using Simple.Infrastructure.Mediator;
global using Simple.Infrastructure.Notifications;
global using Simple.Infrastructure.SqlServer;
global using static Simple.App.Services.ServicesFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.Queue.QueueFuncs;
global using static Simple.Infrastructure.Mediator.MediatorFuncs;
global using static Simple.Infrastructure.Notifications.NotificationsFuncs;
global using static Simple.Shared.Extensions.ExtensionsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;

namespace Simple.Web.Api;

public static partial class ApiFuncs;