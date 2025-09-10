
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static readonly SequentialGuidValueGenerator SequentialGuidGenerator = new();

  public static Guid GenerateSequentialGuid<TContext, TEntity>(TContext dbContext, TEntity entity) where TContext : DbContext =>
    SequentialGuidGenerator.Next(dbContext.Entry(entity!));
}