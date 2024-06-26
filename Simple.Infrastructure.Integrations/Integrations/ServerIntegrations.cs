
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

public sealed record MongoIntegration (IMongoDatabase MongoDatabase, Channel<Message> MongoMessageQueue);

public sealed record SqlIntegration (AgendaContextFactory SqlContextFactory, Channel<Message> SqlMessageQueue);

public sealed record ServerIntegrations (MongoIntegration MongoIntegration, SqlIntegration SqlIntegration);