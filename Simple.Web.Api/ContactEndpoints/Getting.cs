
using System.IO;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static string GetContactCreatedUri (HttpRequest request, Contact contact) => Path.Combine(request.Path, contact.ContactId.ToString());
}