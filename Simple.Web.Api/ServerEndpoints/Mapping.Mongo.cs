
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase agendaDb, Channel<Message> messageQueue)
  {
    app.MapPost("/mongo/contacts", ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, agendaDb, messageQueue, httpContext)).DisableAntiforgery();

    app.MapPost("/mongo/contacts/{contactId}/phonenumbers", (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, agendaDb, httpContext)).DisableAntiforgery();

    return app;
  }
}