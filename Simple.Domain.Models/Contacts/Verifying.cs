
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistContact (Contact? contact) => contact is not null;
}