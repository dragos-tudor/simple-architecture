
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory agendaContextFactory, Channel<Message> messageQueue)
  {
    app.MapPost("/sql/contacts", ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, agendaContextFactory, messageQueue, httpContext)).DisableAntiforgery();

    app.MapPost("/sql/contacts/{contactId}/phonenumbers", (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, agendaContextFactory, httpContext)).DisableAntiforgery();

    return app;
  }
}