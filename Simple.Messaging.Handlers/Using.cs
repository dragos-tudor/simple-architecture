global using System;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Simple.Shared.Models;
global using Simple.Shared.Events;
global using Simple.Infrastructure.MailServer;
global using Simple.Infrastructure.SqlServer;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Shared.Events.EventsFuncs;
global using static Simple.Infrastructure.MailServer.MailServerFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Messaging.Handlers.HandlersFuncs;

namespace Simple.Messaging.Handlers;

public static partial class HandlersFuncs;