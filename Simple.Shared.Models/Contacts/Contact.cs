
namespace Simple.Shared.Models;

public partial record Contact
{
  public Guid ContactId { get; set; }
  public required string ContactEmail { get; set; } = string.Empty;
  public required string ContactName { get; set; } = string.Empty;
  public IList<PhoneNumber> PhoneNumbers { get; init; } = [];
}

