
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task UpdateMessageIsActive (IMongoCollection<Message> coll, Message message, bool isActive, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, message, SetFieldDefinition<Message, bool>(nameof(Message.IsActive), isActive), default, cancellationToken);

  public static Task UpdateMessageFailure (IMongoCollection<Message> coll, Message message, string failureMessage, byte failureCounter, bool isActive = true, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, message,
      CombineDefinitions(
        SetFieldDefinition<Message, string>(nameof(Message.FailureMessage), failureMessage),
        SetFieldDefinition<Message, byte>(nameof(Message.FailureCounter), failureCounter),
        SetFieldDefinition<Message, bool>(nameof(Message.IsActive), isActive)
      ),
    default, cancellationToken);
}