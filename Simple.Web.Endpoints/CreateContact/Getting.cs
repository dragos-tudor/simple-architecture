
namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  static Uri GetContactCreatedUri (Contact contact) => new ("/contacts/" + contact.ContactId.ToString());
}