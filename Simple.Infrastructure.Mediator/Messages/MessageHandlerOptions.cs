
namespace Simple.Infrastructure.Mediator;

public sealed record MessageHandlerOptions (TimeSpan HandleTimeout, byte MaxFailures = 10);