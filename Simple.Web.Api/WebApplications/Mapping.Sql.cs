
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory agendaContextFactory, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost("/sql/contacts", ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, agendaContextFactory, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost("/sql/contacts/{contactId}/phonenumbers", (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, agendaContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapDelete("/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}", (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberSqlEndpoint(contactId, countryCode, number, agendaContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapGet("/sql/contacts/{contactId}", (Guid contactId, HttpContext httpContext) =>
      FindContactSqlEndpoint(contactId, agendaContextFactory, httpContext));

    app.MapGet("/sql/contacts", (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetContactsPageSqlEndpoint(pageIndex, pageSize, agendaContextFactory, httpContext));

    app.MapGet("/sql/messages", (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetMessagesPageSqlEndpoint(pageIndex, pageSize, agendaContextFactory, httpContext));

    return app;
  }
}