
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Created, ProblemHttpResult>> AddPhoneNumberMongoAsync(
    Guid contactId,
    AddPhoneNumberRequest request,
    IMongoDatabase mongoDatabase,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateAddPhoneNumberRequest(request);
    if (ExistErrors(valErrors)) return TypedResults.Problem(JoinErrors(valErrors));

    var contactColl = GetContactCollection(mongoDatabase);
    var phoneNumber = CreatePhoneNumber(request.CountryCode, request.Number, request.Extension, request.NumberType);

    var (_, error) = await AddPhoneNumberAsync(
      contactId,
      phoneNumber,
      (phoneNumber, cancellationToken) => FindMongoPhoneNumber(contactColl, phoneNumber, cancellationToken),
      (contactId, cancellationToken) => FindContactById(contactColl.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, phoneNumber, cancellationToken) => InsertPhoneNumberAsync(contactColl, contact, phoneNumber, cancellationToken),
      cancellationToken);

    return error is null ?
      TypedResults.Created(GetPhoneNumberMongoPath(contactId, phoneNumber.CountryCode, phoneNumber.Number)) :
      TypedResults.Problem(error!);
  }
}