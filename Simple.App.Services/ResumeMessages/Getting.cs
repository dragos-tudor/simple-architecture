
namespace Simple.App.Services;

partial class ServicesFuncs
{
  static DateTime GetMessageStartDate (DateTimeOffset currentDate, TimeSpan startDelay) => (currentDate - startDelay).Date;
}