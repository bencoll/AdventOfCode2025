namespace AdventOfCode.Y2025.Day04;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Printing Department")]
internal class Solution : Solver {
    public object PartOne(string input) {
        var rollsMatrix = input.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

        return rollsMatrix.Select((row, rowNum) =>
            row.Where((t, columnNum) => t == '@' && IsAccessibleRoll(rollsMatrix, rowNum, columnNum)).Count()).Sum();
    }

    public object PartTwo(string input) {
        var rollsMatrix = input.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();

        var totalAccessibleRolls = 0;

        bool hasMoreAccessibleRolls;
        do {
            var currentAccessibleRolls = 0;
            for (var rowNum = 0; rowNum < rollsMatrix.Length; rowNum++) {
                var row = rollsMatrix[rowNum];
                for (var columnNum = 0; columnNum < row.Length; columnNum++) {
                    if (row[columnNum] == '@' && IsAccessibleRoll(rollsMatrix, rowNum, columnNum)) {
                        row[columnNum] = '.';
                        currentAccessibleRolls++;
                    }
                }
            }

            hasMoreAccessibleRolls = currentAccessibleRolls > 0;

            totalAccessibleRolls += currentAccessibleRolls;
        } while (hasMoreAccessibleRolls);


        return totalAccessibleRolls;
    }

    private static bool IsAccessibleRoll(char[][] rollsMatrix, int row, int col) {
        var adjacentPositions = GetAdjacentPositions(rollsMatrix, row, col);

        return adjacentPositions.Count(position => position == '@') < 4;
    }

    private static char[] GetAdjacentPositions(char[][] input, int row, int col) {
        // Add to result in following order:
        //  1. top row
        //  2. middle row
        //  3. bottom row
        // with rows being read left to right
        (int, int)[] positions = [
            (row - 1, col - 1), (row - 1, col), (row - 1, col + 1),
            (row, col - 1), (row, col + 1),
            (row + 1, col - 1), (row + 1, col), (row + 1, col + 1)
        ];

        return positions.Select(position => GetPositionOrDefault(input, position)).ToArray();
    }

    private static char GetPositionOrDefault(char[][] matrix, (int row, int col) position) {
        if (position.row < 0 || position.row >= matrix.Length) {
            return '.';
        }

        if (position.col < 0 || position.col >= matrix[position.row].Length) {
            return '.';
        }

        return matrix[position.row][position.col];
    }
}
