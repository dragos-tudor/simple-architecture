
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static bool ExistMessage (Message? message) => message is not null;
}