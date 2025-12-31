namespace AdventOfCode.Y2025.Day06;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

internal record Problem(string Op, long[] Numbers);

[ProblemName("Trash Compactor")]
internal class Solution : Solver {
    public object PartOne(string input) {
        var rows = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        const int NumberRowCount = 4;
        long total = 0;

        for (var col = 0; col < rows[0].Length; col++) {
            var numbers = new long[NumberRowCount];

            for (var row = 0; row < NumberRowCount; row++) {
                numbers[row] = long.Parse(rows[row][col]);
            }

            var problem = new Problem(rows[NumberRowCount][col], numbers);
            total += SolveProblem(problem);
        }

        return total;
    }

    public object PartTwo(string input) {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var blocks = BuildProblemBlocks(lines).Select(TransposeProblemBlock);

        var problems = blocks.Select(block =>
            new Problem(
                block[0][^1].ToString(),
                block.Select(row => long.Parse(row[..^1])).ToArray()
            )
        );

        return problems.Sum(SolveProblem);
    }

    private static string[] TransposeProblemBlock(string[] block) {
        return Enumerable.Range(0, block[0].Length).Select(i => GetColumn(block, i)).ToArray();
    }

    private static string GetColumn(string[] lines, int col) {
        return string.Join("", lines.Select(line => line[col]));
    }

    private static List<string[]> BuildProblemBlocks(string[] lines) {
        var problemBlocks = new List<string[]>();
        var opsLine = lines[4];

        var blockStart = 0;
        for (var col = 1; col < opsLine.Length; col++) {
            if (opsLine[col] == ' ') {
                continue;
            }

            problemBlocks.Add(GetProblemBlock(lines, blockStart, col - 1));
            blockStart = col;
        }

        problemBlocks.Add(GetProblemBlock(lines, blockStart, opsLine.Length));
        return problemBlocks;
    }

    private static string[] GetProblemBlock(string[] lines, int blockStart, int blockEnd) {
        return lines.Select(line => line[blockStart..blockEnd]).ToArray();
    }

    private static long SolveProblem(Problem problem) {
        return problem.Op switch {
            "+" => problem.Numbers.Sum(),
            "*" => problem.Numbers.Aggregate(1L, (x, y) => x * y),
            _ => 0
        };
    }
}
