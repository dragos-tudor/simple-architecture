#pragma warning disable CA2234

using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Results<Created, ProblemHttpResult>> CreateContactSqlEndpoint (
    Contact contact,
    AgendaContextFactory agendaContextFactory,
    Channel<Message> messageQueue,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    SetContactId(contact, GenerateSequentialGuid(agendaContext, contact));

    var result = await CreateContactService (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers.AsQueryable(), phoneNumbers, cancellationToken),
      (contactName, cancellationToken) => FindContactByName(agendaContext.Contacts.AsQueryable(), contactName).FirstOrDefaultAsync(cancellationToken),
      (contactEmail, cancellationToken) => FindContactByEmail(agendaContext.Contacts.AsQueryable(), contactEmail).FirstOrDefaultAsync(cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessage(agendaContext, contact, message, cancellationToken),
      (message) => ProduceMessage(messageQueue, message),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetSqlContactCreatedPath(FromSuccess(result)!.ContactId)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!), statusCode: 400);
  }
}