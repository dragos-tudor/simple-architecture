
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static string GetInvalidContactEmailError (string contactEmail) => $"Invalid contact email '{contactEmail}'.";

  static string GetMissingContactNameError () => "Missing contact name.";
}