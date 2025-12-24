namespace AdventOfCode.Y2025.Day01;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Secret Entrance")]
internal class Solution : Solver {
    public object PartOne(string input) {
        var position = 50;
        var count = 0;
        foreach (var line in input.Split("\n")) {
            var direction = line[0];
            var displacement = int.Parse(line[1..]);
            if (direction.Equals('L')) {
                displacement *= -1;
            }

            position = (((position + displacement) % 100) + 100) % 100;
            if (position == 0) {
                count++;
            }
        }

        return count;
    }

    public object PartTwo(string input) {
        var position = 50;
        var count = 0;
        foreach (var line in input.Split("\n")) {
            var directionOfRotation = line[0];
            var numberOfMovements = int.Parse(line[1..]);
            var displacement = directionOfRotation.Equals('L') ? -numberOfMovements : numberOfMovements;

            var clicks = 0;

            var grossPosition = position + displacement;

            switch (grossPosition) {
                case > 99:
                    clicks = grossPosition / 100;
                    break;
                case 0:
                    clicks = 1;
                    break;
                case < 0:
                    clicks = (int)Math.Abs(Math.Ceiling((decimal)grossPosition / 100)) + 1;
                    if (position == 0) {
                        clicks--;
                    }

                    break;
            }

            position = ((grossPosition % 100) + 100) % 100;

            count += clicks;
        }

        return count;
    }
}
