namespace Aoc;

[Trait("Day", "02")]
public class Day02
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\\day02_input1.txt";
    
    public Day02(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        _ = _testInput1.Lines().Travel().Distance.Should().Be(150);
        
        var pos = _input1.ReadFileLines().Travel();
        _output.WriteLine(pos.Distance);
    }

    [Fact]
    public void Second()
    {
        _ = _testInput1.Lines().AimAndTravel().Distance.Should().Be(900);

        var pos = _input1.ReadFileLines().AimAndTravel();
        _output.WriteLine(pos.Distance);
    }


    static readonly string _testInput1 = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

}

public record SubmarinePosition(int Length, int Depth, int Aim)
{
    public int Distance => Length * Depth;
}

public static class Day02Ext
{
    public static SubmarinePosition AimAndTravel(this IEnumerable<string> lines)
    {
        var pos = new SubmarinePosition(0, 0, 0);
        foreach (var line in lines)
        {
            var (dir, amt) = line.ParseNav();
            pos = dir! switch
            {
                "up" => pos with { Aim = pos.Aim - amt },
                "down" => pos with { Aim = pos.Aim + amt },
                "forward" => pos with { Length = pos.Length + amt, Depth = pos.Depth + (pos.Aim * amt) },
                _ => throw new ArgumentOutOfRangeException(dir)
            };
        }
        return pos;
    }

    public static SubmarinePosition Travel(this IEnumerable<string> lines)
    {
        var pos = new SubmarinePosition(0, 0, 0);
        foreach (var line in lines)
        {
            var (dir, amt) = line.ParseNav();
            pos = dir! switch
            {
                "up" => pos with { Depth = pos.Depth - amt},
                "down" => pos with { Depth = pos.Depth + amt},
                "forward" => pos with { Length = pos.Length + amt},
                _ => throw new ArgumentOutOfRangeException(dir)
            };
        }
        return pos;
    }

    public static (string Dir, int Amt) ParseNav(this string line)
    {
        var split = line.Split(' ');
        var dir = split[0];
        var amt = int.Parse(split[1]);
        return (dir, amt);
    }

}