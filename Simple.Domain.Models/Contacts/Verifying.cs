
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsContact (Contact? contact) => contact is not null;
}