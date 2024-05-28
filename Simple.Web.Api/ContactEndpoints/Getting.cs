
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static Uri GetContactCreatedUri (Contact contact) => new ("/contacts/" + contact.ContactId.ToString());
}