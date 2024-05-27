
namespace Simple.Web.Endpoints;

public delegate Task PublishMessage<TMessage> (TMessage message);