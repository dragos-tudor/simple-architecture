
namespace Simple.Infrastructure.SqlServer;

public partial class AgendaContext: DbContext
{
  public DbSet<Contact> Contacts => Set<Contact>();
  public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
  public DbSet<Message> Messages => Set<Message>();

  public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Contact>().HasKey(e => e.ContactId);
    modelBuilder.Entity<Contact>().Property(e => e.ContactEmail).HasMaxLength(ContactConstraints.ContactEmailMaxLength);
    modelBuilder.Entity<Contact>().Property(e => e.ContactName).HasMaxLength(ContactConstraints.ContactNameMaxLength);

    modelBuilder.Entity<Message>().HasKey(e => e.MessageId);
    modelBuilder.Entity<Message>().Property(e => e.MessageType).HasMaxLength(MessageContraints.MessageTypeMaxLength);
    modelBuilder.Entity<Message>().Property(e => e.TraceId).HasMaxLength(MessageContraints.TraceIdMaxLength);

    modelBuilder.Entity<PhoneNumber>().HasKey(e => new { e.CountryCode, e.Number });
  }
}