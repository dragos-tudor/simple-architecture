global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Threading.Channels;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MongoDB.Driver;
global using Simple.Domain.Models;
global using Simple.Domain.Services;
global using Simple.Infrastructure.Integrations;
global using static Simple.App.Services.ServicesFuncs;
global using static Simple.Infrastructure.Integrations.IntegrationsFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Simple.Api.Testing")]