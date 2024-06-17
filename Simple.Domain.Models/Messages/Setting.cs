
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message SetMessageCorrelationId (Message message, string correlation) { message.CorrelationId = correlation; return message; }
}