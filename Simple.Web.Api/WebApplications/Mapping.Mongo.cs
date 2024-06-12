
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase agendaDb, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost("/mongo/contacts", (Contact contact, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, agendaDb, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost("/mongo/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, agendaDb, httpContext, logger)).DisableAntiforgery();

    app.MapDelete("/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}", (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberMongoEndpoint(contactId, countryCode, number, agendaDb, httpContext, logger)).DisableAntiforgery();

    app.MapGet("/mongo/contacts/{contactId}", (Guid contactId, HttpContext httpContext) =>
      FindContactMongoEndpoint(contactId, agendaDb, httpContext));

    app.MapGet("/mongo/contacts", (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetContactsPageMongoEndpoint(pageIndex, pageSize, agendaDb, httpContext));

    app.MapGet("/mongo/messages", (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      GetMessagesPageMongoEndpoint(pageIndex, pageSize, agendaDb, httpContext));

    return app;
  }
}