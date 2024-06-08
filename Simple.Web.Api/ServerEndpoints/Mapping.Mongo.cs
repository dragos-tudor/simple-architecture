
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase agendaDb, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost("/mongo/contacts", ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, agendaDb, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost("/mongo/contacts/{contactId}/phonenumbers", (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, agendaDb, httpContext, logger)).DisableAntiforgery();

    return app;
  }
}