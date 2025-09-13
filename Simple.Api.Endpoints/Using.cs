global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Http.HttpResults;
global using FluentValidation;
global using FluentValidation.Results;
global using Simple.Domain.Models;
global using Simple.Shared.Models;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Domain.Services.ServicesFuncs;
global using static Simple.Infrastructure.MessageQueue.MessageQueueFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;

namespace Simple.Api.Endpoints;

public static partial class EndpointsFuncs;