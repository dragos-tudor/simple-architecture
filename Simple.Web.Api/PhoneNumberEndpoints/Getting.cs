#pragma warning disable CA1305

using System.IO;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static string GetPhoneNumberCreatedUri (HttpRequest request, PhoneNumber phoneNumber) => Path.Combine(request.Path, phoneNumber.CountryCode.ToString(), phoneNumber.Number.ToString());
}