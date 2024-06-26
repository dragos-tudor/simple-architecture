
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  static DateTime GetMessageStartDate (DateTimeOffset currentDate, TimeSpan startDelay) => (currentDate - startDelay).Date;
}