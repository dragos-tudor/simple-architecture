
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Testing.Models.ModelsFuncs;
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

[TestClass]
public partial class HandlersTests
{
  static readonly string SqlConnectionString = Storing.SqlServer.SqlServerFuncs.CreateSqlConnectionString("agenda-messaging-handlers", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase("127.0.0.1", 27017, "agenda-messaging-handlers");
  static readonly MailServerOptions MailServerOptions = new();

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _)
  {
    InitializeSqlDatabase("agenda-messaging-handlers", "sa", "P@ssw0rd!", "dbuser", "dbuser.P@ssw0rd!", "127.0.0.1");
    InitializeMongoDatabase();
  }
}