
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static string GetInvalidContactEmailError (string contactEmail) => $"Invalid contact email '{contactEmail}'.";

  public static string GetMissingContactNameError () => "Missing contact name.";
}