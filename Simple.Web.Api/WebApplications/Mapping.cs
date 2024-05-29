
using Microsoft.AspNetCore.Builder;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static WebApplication MapEndpoints (WebApplication app, Func<AgendaContext> createAgendaContext, ProduceMessage<Message> produceMessage)
  {
    app.MapPost("/contacts", (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) =>
      CreateContactEndpoint(contact, phoneNumbers, createAgendaContext, produceMessage, httpContext));

    app.MapPost("/contacts/{contactId}/phonenumbers", (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberEndpoint(contactId, phoneNumber, createAgendaContext, produceMessage, httpContext));

    return app;
  }
}