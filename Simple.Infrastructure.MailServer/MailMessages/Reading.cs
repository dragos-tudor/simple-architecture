
namespace Simple.Infrastructure.EmailServer;

partial class EmailServerFuncs
{
  public static async Task<MimeMessage> ReadMailMessage(ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
  {
    using var stream = new MemoryStream();
    var position = buffer.GetPosition(0);

    while (buffer.TryGet(ref position, out var memory))
      stream.Write(memory.Span);
    stream.Position = 0;

    return await MimeMessage.LoadAsync(stream, cancellationToken);
  }
}