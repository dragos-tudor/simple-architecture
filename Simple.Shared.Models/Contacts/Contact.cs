
namespace Simple.Shared.Models;

public partial record Contact
{
  public Guid ContactId { get; set; }
  public required string ContactEmail { get; set; } = string.Empty;
  public required string ContactName { get; set; } = string.Empty;
  public IList<PhoneNumber> PhoneNumbers { get; init; } = [];
}

partial record Contact
{
  public virtual bool Equals(Contact? contact) =>
    GetType() == contact?.GetType() &&
    ContactId == contact.ContactId &&
    ContactEmail == contact.ContactEmail &&
    ContactName == contact.ContactName &&
    Enumerable.SequenceEqual(PhoneNumbers, contact.PhoneNumbers);

  public override int GetHashCode() => base.GetHashCode();
}