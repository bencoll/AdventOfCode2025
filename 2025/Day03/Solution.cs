namespace AdventOfCode.Y2025.Day03;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Lobby")]
internal class Solution : Solver {
    public object PartOne(string input) {
        return input.Split("\n").Sum(bank => LargestJoltage(bank, 2));
    }

    public object PartTwo(string input) {
        return input.Split("\n").Sum(bank => LargestJoltage(bank, 12));
    }

    private static long LargestJoltage(string bank, int batteryCount) {
        var largestJoltage = 0L;

        while (batteryCount > 0) {
            var battery = bank[..^(batteryCount - 1)].Max();
            bank = bank[(bank.IndexOf(battery) + 1)..];
            largestJoltage = (10 * largestJoltage) + long.Parse(battery.ToString());
            batteryCount--;
        }

        return largestJoltage;
    }
}
