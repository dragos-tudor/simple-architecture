
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> AddPhoneNumberSqlAsync(
    Guid contactId,
    AddPhoneNumberRequest request,
    AgendaContextFactory dbContextFactory,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateAddPhoneNumberRequest(request);
    if (ExistErrors(valErrors)) return TypedResults.Problem(JoinErrors(valErrors));

    var dbContext = CreateAgendaContext(dbContextFactory);
    var phoneNumber = CreatePhoneNumber(request.CountryCode, request.Number, request.Extension, request.NumberType);

    var (_, error) = await AddPhoneNumberService(
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindPhoneNumber(dbContext.PhoneNumbers.AsQueryable(), phoneNumber).FirstOrDefaultAsync(cancellationToken),
      (contactId, cancellationToken) => FindContactById(dbContext.Contacts.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumberSqlAsync(dbContext, contact, phoneNumber, cancellationToken),
      cancellationToken);

    return error is null ?
      TypedResults.Created(GetPhoneNumberSqlPath(contactId, phoneNumber.CountryCode, phoneNumber.Number)) :
      TypedResults.Problem(error!);
  }
}