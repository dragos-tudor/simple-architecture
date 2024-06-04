
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task UpdateMessageIsActive (IMongoCollection<Message> coll, Message message, bool isActive, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, message, SetFieldDefinition<Message, bool>(nameof(Message.IsActive), isActive), default, cancellationToken);
}