
using Microsoft.AspNetCore.Mvc;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  const string SqlContactsPath = "/sql/contacts";
  const string SqlContactPath = "/sql/contacts/{contactId}";
  const string SqlPhoneNumbersPath = "/sql/contacts/{contactId}/phonenumbers";
  const string SqlPhoneNumberPath = "/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
  const string SqlMessagesPath = "/sql/messages";

  public static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory sqlContextFactory, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost(SqlContactsPath, ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, sqlContextFactory, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost(SqlPhoneNumbersPath, (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, sqlContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapDelete(SqlPhoneNumberPath, (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberSqlEndpoint(contactId, countryCode, number, sqlContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapGet(SqlContactPath, (Guid contactId, HttpContext httpContext) =>
      FindContactSqlEndpoint(contactId, sqlContextFactory, httpContext));

    app.MapGet(SqlContactsPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindContactsPageSqlEndpoint(pageIndex, pageSize, sqlContextFactory, httpContext));

    app.MapGet(SqlMessagesPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindMessagesPageSqlEndpoint(pageIndex, pageSize, sqlContextFactory, httpContext));

    return app;
  }
}