
using System.Linq;

namespace Simple.Shared.Models;

sealed partial record Contact
{
  public bool Equals (Contact? other) =>
    other is not null &&
    GetType() == other.GetType() &&
    ContactId == other.ContactId &&
    ContactEmail ==  other.ContactEmail &&
    ContactName == other.ContactName &&
    Enumerable.SequenceEqual(PhoneNumbers, other.PhoneNumbers);

  public override int GetHashCode() => base.GetHashCode();
}