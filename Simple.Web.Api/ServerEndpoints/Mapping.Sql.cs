
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Storing.SqlServer.SqlServerFuncs;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static WebApplication MapSqlEndpoints (WebApplication app, AgendaContextFactory agendaContextFactory, Channel<Message> messageQueue, ILogger logger)
  {
    app.MapPost("/sql/contacts", ([FromForm]Contact contact, HttpContext httpContext) =>
      CreateContactSqlEndpoint(contact, agendaContextFactory, messageQueue, httpContext, logger)).DisableAntiforgery();

    app.MapPost("/sql/contacts/{contactId}/phonenumbers", (Guid contactId, [FromForm]PhoneNumber phoneNumber, HttpContext httpContext) =>
      AddPhoneNumberSqlEndpoint(contactId, phoneNumber, agendaContextFactory, httpContext, logger)).DisableAntiforgery();

    app.MapGet("/mongo/contacts", async (short pageSize, short pageIndex, HttpContext httpContext) => {
      using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
      TypedResults.Ok(agendaContext.Contacts.Include(c => c.PhoneNumbers).Page(pageSize, pageIndex));
    });

    app.MapGet("/mongo/messages", async (short pageSize, short pageIndex, HttpContext httpContext) => {
      using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
      TypedResults.Ok(agendaContext.Messages.Page(pageSize, pageIndex));
    });

    return app;
  }
}