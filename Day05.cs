namespace Aoc;

[Trait("Day", "05")]
public class Day05
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\day05_input1.txt";
    private readonly string _testInput1 = @"inputs\day05_testinput.txt";
    //private readonly string _input2 = @"inputs\\day05_input2.txt";

    public Day05(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        var testLines = _testInput1.ReadFileLines().Select(c => c.GetLine()).ToArray();
        int result = GetGridStuff(testLines);
        result.Should().Be(5);

        var realInput = _input1.ReadFileLines().Select(c => c.GetLine()).ToArray();
        _output.WriteLine(GetGridStuff(realInput));

    }

    [Fact]
    public void Second()
    {
        var testLines = _testInput1.ReadFileLines().Select(c => c.GetLine()).ToArray();
        int result = GetGridStuff(testLines, true);
        result.Should().Be(12);

        var realInput = _input1.ReadFileLines().Select(c => c.GetLine()).ToArray();
        _output.WriteLine(GetGridStuff(realInput, true));
    }

    private int GetGridStuff((int x1, int y1, int x2, int y2)[] points, bool part2 = false)
    {
        var map = new Dictionary<(int X, int Y), int>();

        for (int i = 0; i < points.Length; i++)
        {
            var (x1, y1, x2, y2) = points[i];

            // vertikalen
            if (x1 == x2)
            {
                var min = Math.Min(y1, y2);
                var max = Math.Max(y1, y2);
                for (int y = min; y <= max; y++)
                {
                    if (map.ContainsKey((x1, y)))
                        map[(x1, y)]++;
                    else
                        map[(x1, y)] = 1;
                }
            }
            else if (y1 == y2) // horisontalen
            {
                var min = Math.Min(x1, x2);
                var max = Math.Max(x1, x2);
                for (int x = min; x <= max; x++)
                {
                    if (map.ContainsKey((x, y1)))
                        map[(x, y1)]++;
                    else
                        map[(x, y1)] = 1;
                }
            }
            else if (part2)
            {
                int dx = x1 > x2 ? -1 : 1;
                int dy = y1 > y2 ? -1 : 1;
                int u = 0;
                while (true)
                {
                    var x = x1 + u * dx;
                    var y = y1 + u * dy;
                    if (map.ContainsKey((x, y)))
                        map[(x, y)]++;
                    else
                        map[(x, y)] = 1;
                    if (x == x2 && y == y2)
                        break;
                    u++;
                }
            }
        }

        var c = 0;
        foreach ((var pos, var pts) in map)
        {
            if (pts >= 2)
                c++;
        }

        return c;
    }



}

public static class Day05Ext
{
    public static (int X1, int Y1, int X2, int Y2) GetLine(this string input)
    {
        var split = input.Split(" -> ");
        var first = split[0].Split(',').Select(c => int.Parse(c)).ToArray();
        var second = split[1].Split(',').Select(c => int.Parse(c)).ToArray();
        return (first[0], first[1], second[0], second[1]);
    }
}