
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

public delegate Subscriber<Message>[] RegisterMongoSubscribers(IMongoDatabase mongoDatabase, EmailServerOptions emailServerOptions, Channel<Message> messageQueue, ILogger queueLogger, TimeProvider? timeProvider = default);

public delegate Subscriber<Message>[] RegisterSqlSubscribers(AgendaContextFactory sqlContextFactory, EmailServerOptions emailServerOptions, Channel<Message> messageQueue, ILogger queueLogger, TimeProvider? timeProvider = default);