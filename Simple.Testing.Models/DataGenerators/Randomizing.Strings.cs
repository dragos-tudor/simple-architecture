
namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  static char GetRandomPrintableChar(int _) => (char)GetRandomInt(33, 126);

  public static string GetRandomEmail(int length) => GetRandomLetters(length - 8) + "@" + GetRandomLetters(4) + "." + GetRandomLetters(2);

  static char GetRandomLetter(int _) => (char)GetRandomInt(97, 122);

  public static string GetRandomLetters(int length) => new([.. Range(0, length).Select(GetRandomLetter)]);

  public static string GetRandomString(int length) => new([.. Range(0, length).Select(GetRandomPrintableChar)]);
}
