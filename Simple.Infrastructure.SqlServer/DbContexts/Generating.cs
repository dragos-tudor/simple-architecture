
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static readonly SequentialGuidValueGenerator GuidGenerator = new ();

  public static Guid GenerateEntityId<TContext, TEntity> (TContext dbContext, TEntity entity) where TContext: DbContext =>
    GuidGenerator.Next(dbContext.Entry(entity!));
}