
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  const string MongoContactsPath = "/mongo/contacts";
  const string MongoContactPath = "/mongo/contacts/{contactId}";
  const string MongoPhoneNumbersPath = "/mongo/contacts/{contactId}/phonenumbers";
  const string MongoPhoneNumberPath = "/mongo/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
  const string MongoMessagesPath = "/mongo/messages";

  public static WebApplication MapMongoEndpoints (WebApplication app, IMongoDatabase mongoDatabase, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost(MongoContactsPath, (Contact contact, HttpContext httpContext) =>
      CreateContactMongoEndpoint(contact, mongoDatabase, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost(MongoPhoneNumbersPath, (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberMongoEndpoint(contactId, phoneNumber, mongoDatabase, httpContext, logger)).DisableAntiforgery();

    app.MapDelete(MongoPhoneNumberPath, (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberMongoEndpoint(contactId, countryCode, number, mongoDatabase, httpContext, logger)).DisableAntiforgery();

    app.MapGet(MongoContactPath, (Guid contactId, HttpContext httpContext) =>
      FindContactMongoEndpoint(contactId, mongoDatabase, httpContext));

    app.MapGet(MongoContactsPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindContactsPageMongoEndpoint(pageIndex, pageSize, mongoDatabase, httpContext));

    app.MapGet(MongoMessagesPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindMessagesPageMongoEndpoint(pageIndex, pageSize, mongoDatabase, httpContext));

    return app;
  }
}







