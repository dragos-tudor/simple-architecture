
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static Storing.MongoDb.MongoDbFuncs;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase agendaDb, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost("/mongo/contacts", (Contact contact, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, agendaDb, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost("/mongo/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, agendaDb, httpContext, logger)).DisableAntiforgery();

    app.MapGet("/mongo/contacts", (short pageSize, short pageIndex, HttpContext httpContext) =>
      TypedResults.Ok(GetContactCollection(agendaDb).AsQueryable().Page(pageSize, pageIndex)));

    app.MapGet("/mongo/messages", (short pageSize, short pageIndex, HttpContext httpContext) =>
      TypedResults.Ok(GetMessageCollection(agendaDb).AsQueryable().Page(pageSize, pageIndex)));

    return app;
  }
}