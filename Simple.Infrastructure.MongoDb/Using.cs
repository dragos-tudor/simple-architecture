global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;
global using Simple.Domain.Models;
global using static Docker.Extensions.DockerFuncs;
global using static Storing.MongoDb.MongoDbFuncs;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;

namespace Simple.Infrastructure.MongoDb;

public static partial class MongoDbFuncs;