#pragma warning disable CA2234

using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> CreateContactSqlEndpoint (
    Contact contact,
    AgendaContextFactory sqlContextFactory,
    Channel<Message> messageQueue,
    HttpContext httpContext,
    ILogger logger)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    SetContactId(contact, GenerateSequentialGuid(agendaContext, contact));

    var result = await CreateContactService (
      contact,
      (phoneNumbers, cancellationToken) => FindPhoneNumbers(agendaContext.PhoneNumbers.AsQueryable(), phoneNumbers, cancellationToken),
      (contactName, cancellationToken) => FindContactByName(agendaContext.Contacts.AsQueryable(), contactName).FirstOrDefaultAsync(cancellationToken),
      (contactEmail, cancellationToken) => FindContactByEmail(agendaContext.Contacts.AsQueryable(), contactEmail).FirstOrDefaultAsync(cancellationToken),
      (contact, message, cancellationToken) => InsertContactAndMessageAsync(agendaContext, contact, message, cancellationToken),
      (message) => EnqueueMessage(messageQueue, message),
      logger,
      httpContext.TraceIdentifier,
      httpContext.RequestAborted);

    return IsSuccessResult(result)?
      TypedResults.Created(GetSqlContactCreatedPath(FromSuccess(result)!.ContactId)):
      TypedResults.Problem(JoinFailures(FromFailure(result)!), statusCode: 400);
  }
}