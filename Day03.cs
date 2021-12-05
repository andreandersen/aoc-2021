namespace Aoc;

[Trait("Day", "03")]
public class Day03
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\day03_input1.txt";
    static readonly string _testInput1 = @"inputs\day03_testinput.txt";

    public Day03(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        var (gaTest, epsTest) = _testInput1.ReadFileLines().GetPower();

        _ = gaTest.Should().Be(22);
        _ = epsTest.Should().Be(9);

        var (ga, ep) = _input1.ReadFileLines().GetPower();
        _output.WriteLine(ga * ep);
    }

    [Fact]
    public void Second()
    {
        var (o2test, co2test) = _testInput1.ReadFileLines().GetLifeSupport();
        _ = (o2test * co2test).Should().Be(230);

        var (o2, co2) = _input1.ReadFileLines().GetLifeSupport();
        _output.WriteLine(o2 * co2);
    }
}

public static class Day03Ext
{
    public static (int, int) GetLifeSupport(this IEnumerable<string> lines)
    {
        var oxygen = lines.DiagnosticsReduce(MostLeast.Most);
        var co2 = lines.DiagnosticsReduce(MostLeast.Lest);
        return (Convert.ToInt32(oxygen, 2), Convert.ToInt32(co2, 2));
    }

    public enum MostLeast { Most, Lest }

    public static string DiagnosticsReduce(
        this IEnumerable<string> lines, MostLeast mode)
    {
        var list = lines.ToList();
        var width = list[0].Length;

        for (int i = 0; i < width; i++)
        {
            if (list.Count == 1)
                break;

            var one = new List<string>();
            var zero = new List<string>();

            foreach (var row in list)
            {
                if (row[i] == '1')
                    one.Add(row);
                else
                    zero.Add(row);
            }

            list = mode == MostLeast.Most
                ? one.Count >= zero.Count ? one : zero
                : one.Count < zero.Count ? one : zero;
        }

        return list.Single();
    }

    public static (int, int) GetPower(this IEnumerable<string> lines)
    {
        var arr = lines.ToArray();
        var width = arr[0].Length;
        var gamma = new char[width].AsSpan();
        var epsilon = new char[width].AsSpan();

        for (int i = 0; i < width; i++)
        {
            int c0 = 0, c1 = 0;
            foreach (var row in arr)
            {
                if (row[i] == '0')
                    c0++;
                else
                    c1++;
            }

            gamma[i] = c1 > c0 ? '1' : '0';
            epsilon[i] = c0 > c1 ? '1' : '0';
        }

        return (
            Convert.ToInt32(gamma.ToString(), 2),
            Convert.ToInt32(epsilon.ToString(), 2));
    }
}