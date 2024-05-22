
namespace Simple.Shared.Models;

public record Contact
{
  public Guid ContactId { get; set; }
  public required string ContactEmail { get; set; } = string.Empty;
  public required string ContactName { get; set; } = string.Empty;
  public IEnumerable<PhoneNumber>? PhoneNumbers { get; set; }
}

