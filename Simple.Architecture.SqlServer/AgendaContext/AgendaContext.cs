
using Microsoft.EntityFrameworkCore;

namespace Simple.Architecture.SqlServer;

public class AgendaContext: DbContext
{
  public DbSet<ContactDb> Contacts => Set<ContactDb>();
  public DbSet<MessageDb> Messages => Set<MessageDb>();
  public DbSet<PhoneNumberDb> PhoneNumbers => Set<PhoneNumberDb>();
}