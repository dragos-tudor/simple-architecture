
namespace Simple.Infrastructure.EmailServer;

public delegate MimeMessage MapMail<TMail>(TMail mail);

public delegate TMail MapMessage<TMail>(MimeMessage message);

public delegate Task<IEnumerable<TMail>> ReceiveMails<TMail> (string userName, string password, Predicate<TMail> filterMail, CancellationToken cancellationToken = default);

public delegate Task SendMail<TMail> (TMail mail, CancellationToken cancellationToken = default);