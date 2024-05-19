
namespace Simple.Architecture.Mediator;

public class Message<TPayload>: Message
{
  public TPayload MessagePayload { get; set; } = default!;
}