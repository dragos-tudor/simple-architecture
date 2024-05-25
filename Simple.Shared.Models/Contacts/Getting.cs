
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string GetInvalidContactEmailError (string contactEmail) => $"Invalid contact email '{contactEmail}'.";

  static string GetMissingContactNameError () => "Missing contact name.";
}