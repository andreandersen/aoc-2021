namespace Aoc;

public static class Extensions
{
    public static IEnumerable<int> ToInts(this IEnumerable<string> src) =>
        src.Select(c => int.Parse(c));

    public static IEnumerable<string> ReadFileLines(this string filename) =>
        File.ReadLines(filename);

    public static IEnumerable<string> Lines(this string src) =>
        src.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);


    public static void WriteLine(this ITestOutputHelper output, int src) =>
        output.WriteLine($"{src}");

    public static void WriteLine(this ITestOutputHelper output, IEnumerable<int> src) =>
        output.WriteLine(string.Join("\r\n", src));

    public static void Write(this ITestOutputHelper output, IEnumerable<string> src) =>
        output.WriteLine(string.Join("\r\n", src));
}
