global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Simple.Domain.Models;
global using Simple.Domain.Services;
global using Simple.Infrastructure.Integrations;
global using static Simple.Domain.Integrations.IntegrationsFuncs;
global using static Simple.Infrastructure.Integrations.IntegrationsFuncs;

namespace Simple.Api;

public static partial class ApiFuncs;