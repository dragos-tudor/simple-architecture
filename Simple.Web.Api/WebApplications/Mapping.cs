
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static WebApplication MapEndpoints (WebApplication app, Func<AgendaContext> createAgendaContext, ProduceMessage<Message> produceMessage)
  {
    app.MapPost("/contacts", async (Contact contact, IEnumerable<PhoneNumber> phoneNumbers, HttpContext httpContext) => {
      using var agendaContext = createAgendaContext();
      return await CreateContactEndpoint (
        contact,
        phoneNumbers,
        (phoneNumbers, cancellationToken) => FindPhoneNumbersWithPhoneNumbers(agendaContext.PhoneNumbers, phoneNumbers, cancellationToken),
        (contact, message, cancellationToken) => AddAndStoreContactAndMessage(agendaContext, contact, message, cancellationToken),
        produceMessage,
        httpContext);
    });

    app.MapPost("/contacts/{contactId}/phonenumbers", async (Guid contactId, PhoneNumber phoneNumber, HttpContext httpContext) => {
      using var agendaContext = createAgendaContext();
      return await AddPhoneNumberEndpoint (
        contactId,
        phoneNumber,
        (phoneNumber, cancellationToken) => FindPhoneNumber(agendaContext.PhoneNumbers, phoneNumber).FirstOrDefaultAsync(cancellationToken),
        (contactId, cancellationToken) => FindContactById(agendaContext.Contacts, contactId).FirstOrDefaultAsync(cancellationToken),
        (contact, phoneNumber, cancellationToken) => AddAndStorePhoneNumber(agendaContext, contact, phoneNumber, cancellationToken),
        httpContext);
    });

    return app;
  }
}