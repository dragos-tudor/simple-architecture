
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static WebApplication MapSqlEndpoints (WebApplication app, Func<AgendaContext> createAgendaContext, ProduceMessage<Message> produceMessage)
  {
    app.MapPost("/contacts", (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, phoneNumbers, createAgendaContext, produceMessage, httpContext));

    app.MapPost("/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, createAgendaContext, produceMessage, httpContext));

    return app;
  }

  static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase db, ProduceMessage<Message> produceMessage)
  {
    app.MapPost("/contacts", (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, phoneNumbers, GetContactCollection(db), GetMessageCollection(db), produceMessage, httpContext));

    app.MapPost("/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, GetContactCollection(db), produceMessage, httpContext));

    return app;
  }
}