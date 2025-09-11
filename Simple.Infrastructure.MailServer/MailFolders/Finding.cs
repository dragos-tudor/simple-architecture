
using MailKit;
using MailKit.Search;

namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  // static Task<IList<UniqueId>> FindAllMessages(IMailFolder mailFolder, CancellationToken cancellationToken) => FindMessages(mailFolder, SearchQuery.All, cancellationToken);

  static Task<IList<UniqueId>> FindMessages(IMailFolder mailFolder, SearchQuery searchQuery, CancellationToken cancellationToken) => mailFolder.SearchAsync(searchQuery, cancellationToken);

  static Task<IList<UniqueId>> FindUnseenMessages(IMailFolder mailFolder, CancellationToken cancellationToken) => FindMessages(mailFolder, SearchQuery.NotSeen, cancellationToken);
}