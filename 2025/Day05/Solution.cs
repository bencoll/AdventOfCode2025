namespace AdventOfCode.Y2025.Day05;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

internal record Range(long Start, long End);

[ProblemName("Cafeteria")]
internal class Solution : Solver {
    public object PartOne(string input) {
        var (ranges, availableIds) = ParseRanges(input);
        return availableIds.Count(id =>
            ranges.Any(range => range.Start <= id && id <= range.End));
    }

    public object PartTwo(string input) {
        var ranges = ParseRanges(input).ranges.OrderBy(range => range.Start).ThenBy(range => range.End).ToArray();

        for (var i = 0; i < ranges.Length - 1; i++) {
            if (ranges[i + 1].Start > ranges[i].End) {
                continue;
            }

            var end = Math.Max(ranges[i].End, ranges[i + 1].End);
            ranges[i] = new Range(ranges[i].Start, ranges[i + 1].Start - 1);
            ranges[i + 1] = new Range(ranges[i + 1].Start, end);
        }

        return ranges.Select(r => r.End - r.Start + 1).Sum();
    }

    private static (Range[] ranges, long[] availableIds) ParseRanges(string input) {
        var lines = input.Split(Environment.NewLine);

        var splitIndex = lines.IndexOf("");
        var ranges = lines[..splitIndex].Select(range => {
            var r = range.Split('-');
            return new Range(long.Parse(r[0]), long.Parse(r[1]));
        }).ToArray();
        var availableIds = lines[(splitIndex + 1)..].Select(long.Parse).ToArray();

        return (ranges, availableIds.ToArray());
    }
}
