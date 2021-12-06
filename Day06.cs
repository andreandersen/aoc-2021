namespace Aoc;

[Trait("Day", "06")]
public class Day06
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\\day06_input1.txt";
    private readonly string _testInput1 = @"inputs\day06_testinput.txt";

    public Day06(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        _ = _testInput1.ReadFileLines().SpawnFish(80).Should().Be(5934);
        var result = _input1.ReadFileLines().SpawnFish(80);
        _output.WriteLine(result.ToString());
    }

    [Fact]
    public void Second()
    {
        _ = _testInput1.ReadFileLines().SpawnFish(256).Should().Be(26984457539);
        var result = _input1.ReadFileLines().SpawnFish(256);
        _output.WriteLine(result.ToString());
    }
}

public static class Day06Ext
{
    public static long SpawnFish(this IEnumerable<string> input, int days)
    {
        var x = input
            .First()
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => int.Parse(c))
            .GroupBy(c => c)
            .ToDictionary(c => c.Key, c => c.Count());

        var f = Enumerable.Range(0, 9)
            .ToDictionary(
                c => c, 
                c => (long)(x.TryGetValue(c, out int n) ? n : 0));

        for (int i = 0; i < days; i++)
        {
            var spawn = f[0];
            for (int j = 0; j < 8; j++)
                f[j] = f[j + 1];

            f[6] += spawn;
            f[8] = spawn;
        }

        return f.Sum(c => c.Value); ;
    }
}