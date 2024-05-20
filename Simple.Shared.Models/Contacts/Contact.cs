
namespace Simple.Shared.Models;

public class Contact
{
  public Guid ContactId { get; set; }
  public string ContactEmail { get; set; } = string.Empty;
  public string ContactName { get; set; } = string.Empty;
  public IEnumerable<PhoneNumber>? PhoneNumbers { get; set; }
}

