
using Microsoft.AspNetCore.Mvc;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  const string SqlContactsPath = "/sql/contacts";
  const string SqlContactPath = "/sql/contacts/{contactId}";
  const string SqlPhoneNumbersPath = "/sql/contacts/{contactId}/phonenumbers";
  const string SqlPhoneNumberPath = "/sql/contacts/{contactId}/phonenumbers/{countryCode}/{number}";
  const string SqlMessagesPath = "/sql/messages";

  public static WebApplication MapSqlEndpoints(WebApplication app, AgendaContextFactory sqlContextFactory, Channel<Message> messageQueue)
  {
    app.MapPost(SqlContactsPath, ([FromForm] CreateContactRequest request, HttpContext httpContext) =>
      CreateContactSqlAsync(request, sqlContextFactory, messageQueue, httpContext.TraceIdentifier, httpContext.RequestAborted)).DisableAntiforgery();

    app.MapPost(SqlPhoneNumbersPath, (Guid contactId, [FromForm] AddPhoneNumberRequest request, HttpContext httpContext) =>
      AddPhoneNumberSqlAsync(contactId, request, sqlContextFactory, httpContext.RequestAborted)).DisableAntiforgery();

    app.MapDelete(SqlPhoneNumberPath, (Guid contactId, short countryCode, long number, HttpContext httpContext) =>
      DeletePhoneNumberSqlAsync(contactId, countryCode, number, sqlContextFactory, httpContext.RequestAborted)).DisableAntiforgery();

    // testing purpose
    app.MapGet(SqlContactPath, (Guid contactId, HttpContext httpContext) =>
      FindContactSqlAsync(contactId, sqlContextFactory, httpContext.RequestAborted));

    app.MapGet(SqlContactsPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindContactsSqlAsync(pageIndex, pageSize, sqlContextFactory, httpContext.RequestAborted));

    app.MapGet(SqlMessagesPath, (short? pageIndex, short? pageSize, HttpContext httpContext) =>
      FindMessagesSqlAsync(pageIndex, pageSize, sqlContextFactory, httpContext.RequestAborted));

    return app;
  }
}