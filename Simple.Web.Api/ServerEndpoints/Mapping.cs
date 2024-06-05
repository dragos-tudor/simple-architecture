
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory agendaContextFactory, Channel<Message> messageQueue)
  {
    app.MapPost("/sql/contacts", (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, phoneNumbers, agendaContextFactory, messageQueue, httpContext));

    app.MapPost("/sql/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, agendaContextFactory, httpContext));

    return app;
  }

  internal static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase agendaDb, Channel<Message> messageQueue)
  {
    app.MapPost("/mongo/contacts", (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, phoneNumbers, agendaDb, messageQueue, httpContext));

    app.MapPost("/mongo/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, agendaDb, httpContext));

    return app;
  }
}