
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static bool SetMessageIsActive (Message message, bool isActive) => message.IsActive = isActive;
}