
namespace Simple.Api;

public record MongoOptions
{
  public string DbName { get; init; } = default!;
  public int ServerPort { get; init; } = 27017;
  public string ServerName { get; init; } = "127.0.0.1";
}