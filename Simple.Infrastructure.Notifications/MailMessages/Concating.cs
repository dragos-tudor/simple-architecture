
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static string ConcatMailAdresses (IEnumerable<string> mailAddresses) => string.Join(',', mailAddresses);
}