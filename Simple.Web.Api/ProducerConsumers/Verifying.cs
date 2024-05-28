
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static bool ExistDispatchError (string error) => !string.IsNullOrEmpty(error);
}