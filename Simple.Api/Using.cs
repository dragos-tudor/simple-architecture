global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using MongoDB.Driver;
global using Simple.App.Integrations;
global using Simple.Domain.Models;
global using Simple.Domain.Services;
global using static Simple.App.Integrations.IntegrationsFuncs;
global using static Simple.App.Services.ServicesFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Simple.Api.Testing")]