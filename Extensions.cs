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

    public static void WriteLine(this ITestOutputHelper output, IEnumerable<string> src) =>
        output.WriteLine(string.Join("\r\n", src));

    public static int Count(this ReadOnlySpan<char> input, ReadOnlySpan<char> match)
    {
        int count = 0;
        var haystack = input;
        
        while (true)
        {
            var x = haystack.IndexOf(match);
            if (x == -1)
                break;

            count++;
            haystack = haystack.Slice(x + match.Length);
        }

        return count;
    }

    public static CharSpanSplitter Split(this ReadOnlySpan<char> input)
        => new CharSpanSplitter(input);

    public static CharSpanSplitter Split(this Span<char> input)
        => new CharSpanSplitter(input);
}

public readonly ref struct CharSpanSplitter
{
    readonly ReadOnlySpan<char> _input;
    public CharSpanSplitter(ReadOnlySpan<char> input) => _input = input;
    public Rator GetEnumerator() => new Rator(_input);

    public ref struct Rator   // Forward-only enumerator
    {
        readonly ReadOnlySpan<char> _input;
        int _wordPos;
        public ReadOnlySpan<char> Current { get; private set; }

        public Rator(ReadOnlySpan<char> input)
        {
            _input = input;
            _wordPos = 0;
            Current = default;
        }

        public bool MoveNext()
        {
            for (int i = _wordPos; i <= _input.Length; i++)
                if (i == _input.Length || char.IsWhiteSpace(_input[i]))
                {
                    Current = _input[_wordPos..i];
                    _wordPos = i + 1;
                    return true;
                }
            return false;
        }
    }
}
