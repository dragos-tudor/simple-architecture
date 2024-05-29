
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static bool ExistNetwork (NetworkResponse? networkResponse) => networkResponse is not null;
}