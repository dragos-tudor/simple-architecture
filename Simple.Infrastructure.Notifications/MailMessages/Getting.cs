
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static IEnumerable<string> GetMailAddresses (InternetAddressList internetAddresses) => internetAddresses.Select(address => address.Name);
}