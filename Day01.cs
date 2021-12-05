namespace Aoc;

[Trait("Day", "01")]
public class Day01
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\day01_input1.txt";

    public Day01(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        _ = _testInput1.GetIncreases().Should().Be(7);

        var res = _input1.ReadFileLines().ToInts().GetIncreases();
        _output.WriteLine(res);
    }

    [Fact]
    public void Second()
    {
        _ = _testInput1.GetSlidingWindows().SequenceEqual(_slidingExpected);
        _ = _testInput1.GetSlidingWindows().GetIncreases().Should().Be(5);

        var res = _input1
            .ReadFileLines().ToInts()
            .GetSlidingWindows().GetIncreases();
        _output.WriteLine(res);
    }

    static readonly int[] _testInput1 = new [] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
    static readonly int[] _slidingExpected = new [] { 607, 618, 617, 647, 716, 769, 792 };
}

public static class Day01Ext
{
    public static int GetIncreases(this IEnumerable<int> measures) =>
        measures.Pairwise((a, b) => b > a).Count(a => a);

    public static IEnumerable<int> GetSlidingWindows(this IEnumerable<int> measures) =>
        measures.Window(3).Select(p => p.Sum());
}