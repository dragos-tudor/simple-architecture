
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;
global using Simple.Domain.Models;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.SqlServer.SqlServerFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

public static partial class SqlServerFuncs;