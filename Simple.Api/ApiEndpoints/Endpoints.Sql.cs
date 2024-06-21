
using Microsoft.AspNetCore.Mvc;

namespace Simple.Api;

partial class ApiFuncs
{
  const string SqlContactsPath = "/sql/contacts";
  const string SqlContactPath = "/sql/contacts/{contactId}";
  const string SqlPhoneNumbersPath = "/sql/contacts/{contactId}/phonenumbers";
  const string SqlPhoneNumberPath = "/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
  const string SqlMessagesPath = "/sql/messages";

  static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory agendaContextFactory, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost(SqlContactsPath, ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, agendaContextFactory, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost(SqlPhoneNumbersPath, (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, agendaContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapDelete(SqlPhoneNumberPath, (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberSqlEndpoint(contactId, countryCode, number, agendaContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapGet(SqlContactPath, (Guid contactId, HttpContext httpContext) =>
      FindContactSqlEndpoint(contactId, agendaContextFactory, httpContext));

    app.MapGet(SqlContactsPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetContactsPageSqlEndpoint(pageIndex, pageSize, agendaContextFactory, httpContext));

    app.MapGet(SqlMessagesPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetMessagesPageSqlEndpoint(pageIndex, pageSize, agendaContextFactory, httpContext));

    return app;
  }
}