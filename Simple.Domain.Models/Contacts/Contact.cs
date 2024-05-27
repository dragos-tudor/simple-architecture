#pragma warning disable CA1725

namespace Simple.Domain.Models;

public partial record Contact
{
  public Guid ContactId { get; set; }
  public required string ContactEmail { get; set; } = string.Empty;
  public required string ContactName { get; set; } = string.Empty;
  public IList<PhoneNumber> PhoneNumbers { get; init; } = [];
}

sealed partial record Contact
{
  public bool Equals (Contact? contact) =>
    GetType() == contact?.GetType() &&
    ContactId == contact.ContactId &&
    ContactEmail ==  contact.ContactEmail &&
    ContactName == contact.ContactName &&
    Enumerable.SequenceEqual(PhoneNumbers, contact.PhoneNumbers);

  public override int GetHashCode() => base.GetHashCode();
}