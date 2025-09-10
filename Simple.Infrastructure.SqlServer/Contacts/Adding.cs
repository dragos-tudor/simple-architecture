
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Contact AddContact(AgendaContext dbContext, Contact contact) => AddEntity(dbContext, contact);
}