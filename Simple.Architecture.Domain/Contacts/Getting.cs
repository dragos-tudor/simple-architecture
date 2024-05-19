
namespace Simple.Architecture.Domain;

partial class DomainFuncs
{
  static string GetInvalidContactEmailError (string contactEmail) => $"Invalid contact email '{contactEmail}'.";

  static string GetMissingContactNameError () => "Missing contact name.";
}