
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  const string MongoContactsPath = "/mongo/contacts";
  const string MongoContactPath = "/mongo/contacts/{contactId}";
  const string MongoPhoneNumbersPath = "/mongo/contacts/{contactId}/phonenumbers";
  const string MongoPhoneNumberPath = "/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
  const string MongoMessagesPath = "/mongo/messages";

  public static WebApplication MapMongoEndpoints(WebApplication app, IMongoDatabase mongoDatabase, Channel<Message> messageQueue)
  {
    app.MapPost(MongoContactsPath, (CreateContactRequest request, HttpContext httpContext) =>
      CreateContactMongoAsync(request, mongoDatabase, messageQueue, httpContext.TraceIdentifier, httpContext.RequestAborted)).DisableAntiforgery();

    app.MapPost(MongoPhoneNumbersPath, (Guid contactId, AddPhoneNumberRequest request, HttpContext httpContext) =>
      AddPhoneNumberMongoAsync(contactId, request, mongoDatabase, httpContext.RequestAborted)).DisableAntiforgery();

    app.MapDelete(MongoPhoneNumberPath, (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberMongoAsync(contactId, countryCode, number, mongoDatabase, httpContext.RequestAborted)).DisableAntiforgery();

    // testing purpose
    app.MapGet(MongoContactPath, (Guid contactId, HttpContext httpContext) =>
      FindContactMongoAsync(contactId, mongoDatabase, httpContext.RequestAborted));

    app.MapGet(MongoContactsPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindContactsMongoAsync(pageIndex, pageSize, mongoDatabase, httpContext.RequestAborted));

    app.MapGet(MongoMessagesPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindMessagesMongoAsync(pageIndex, pageSize, mongoDatabase, httpContext.RequestAborted));

    return app;
  }
}







