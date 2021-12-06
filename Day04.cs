using System.Numerics;
using System.Text;

namespace Aoc;

[Trait("Day", "04")]
public class Day04
{
    private readonly ITestOutputHelper _output;
    private readonly string _input1 = @"inputs\day04_input1.txt";
    private readonly string _testInput1 = @"inputs\day04_testinput.txt";

    public Day04(ITestOutputHelper output) => _output = output;

    [Fact]
    public void First()
    {
        _ = new BingoGame(_testInput1.ReadFileLines()).FirstToWin().Should().Be(4512);
        _output.WriteLine(new BingoGame(_input1.ReadFileLines()).FirstToWin());
    }

    [Fact]
    public void Second()
    {
        _ = new BingoGame(_testInput1.ReadFileLines()).LastToWin().Should().Be(1924);
        _output.WriteLine(new BingoGame(_input1.ReadFileLines()).LastToWin());
    }
}

public sealed class BingoGame
{
    public BingoBoard[] Boards { get; }

    public BingoGame(IEnumerable<string> input)
    {
        var nums = input
            .First().Split(',')
            .Select(c => int.Parse(c)).ToArray();

        Boards = input
            .Skip(2).Chunk(5 + 1)
            .Select(c => new BingoBoard(c.Take(5).ToArray()))
            .ToArray();

        var winners = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            foreach (var board in Boards)
            {
                var won = board.Draw(nums[i], winners);
                if (won) winners++;
            }
        }
    }

    public int FirstToWin() => Boards
        .OrderBy(c => c.WinIndex)
        .First().Score;

    public int LastToWin() => Boards
        .OrderByDescending(c => c.WinIndex)
        .First().Score;
}

public sealed class BingoBoard
{
    public BingoCell[] Board { get; }
    public int Score { get; private set; }
    public bool HasWon { get; private set; }
    public int WinIndex { get; private set; }

    public BingoBoard(string[] lines)
    {
        Board = new BingoCell[25];
        var i = 0;
        foreach (var line in lines)
        {
            var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var num in nums)
            {
                Board[i++] = new BingoCell(int.Parse(num), false);
            }
        }
    }

    public bool Draw(int number, int winners)
    {
        if (HasWon) return false;
        if (FindAndMark(number) && HasBingo())
        {
            Score = CalculateScore(number);
            HasWon = true;
            WinIndex = winners;
            return true;
        }
        return false;
    }

    private bool FindAndMark(int number)
    {
        var idx = Board.AsSpan().IndexOf(
            new BingoCell(number, false));

        if (idx == -1) return false;

        Board[idx] = Board[idx] with { Marked = true };
        return true;
    }

    private bool HasBingo()
    {
        var b = Board.AsSpan();
        for (int i = 0; i < 5; i++)
        {
            var r = i * 5;
            if (b[r].Marked && b[r + 1].Marked && b[r + 2].Marked 
                && b[r + 3].Marked && b[r + 4].Marked)
                return true;

            if (b[i].Marked && b[i + 5].Marked && b[i + 10].Marked 
                && b[i + 15].Marked && b[i + 20].Marked)
                return true;
        }
        return false;
    }

    private int CalculateScore(int finalNumber)
    {
        var score = 0;
        foreach (var c in Board)
            if (!c.Marked) score += c.Num;
        return score * finalNumber;
    }

}

public readonly record struct BingoCell(int Num, bool Marked);