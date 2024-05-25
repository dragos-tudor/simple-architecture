#pragma warning disable CA1725

namespace Simple.Shared.Models;

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