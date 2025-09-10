
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task UpdateMessageIsPendingAsync(IMongoCollection<Message> coll, Message message, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, message, SetFieldDefinition<Message, bool>(nameof(Message.IsPending), message.IsPending), default, cancellationToken);

  public static Task UpdateMessageErrorAsync(IMongoCollection<Message> coll, Message message, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, message,
      CombineDefinitions(
        SetFieldDefinition<Message, string>(nameof(Message.ErrorMessage), message.ErrorMessage),
        SetFieldDefinition<Message, byte>(nameof(Message.ErrorCounter), message.ErrorCounter ?? 0),
        SetFieldDefinition<Message, bool>(nameof(Message.IsPending), message.IsPending)
      ),
    default, cancellationToken);
}