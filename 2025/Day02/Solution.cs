namespace AdventOfCode.Y2025.Day02;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Gift Shop")]
internal class Solution : Solver {
    public object PartOne(string input) {
        var ranges = ValueTuples(input);

        return ranges.SelectMany(range => FindInvalidIdsInRangeWithRepetitions(range, _ => 2)).Sum();
    }

    public object PartTwo(string input) {
        var ranges = ValueTuples(input);

        return ranges.SelectMany(range => FindInvalidIdsInRangeWithRepetitions(range, st => st.Length)).Sum();
    }

    private static IEnumerable<(long Start, long End)> ValueTuples(string input) {
        var ranges =
            from range in input.Split(',')
            let parts = range.Split('-')
            select (
                Start: long.Parse(parts[0]),
                End: long.Parse(parts[1])
            );
        return ranges;
    }

    private static List<long> FindInvalidIdsInRangeWithRepetitions((long Start, long End) range,
        Func<string, int> getNumberOfRepetitions) {
        var invalidIds = new List<long>();
        for (var i = range.Start; i <= range.End; i++) {
            var numberOfRepetitions = getNumberOfRepetitions(i.ToString());
            for (var j = 2; j <= numberOfRepetitions; j++) {
                if (IdContainsRepeatedSequence(i.ToString(), j)) {
                    invalidIds.Add(i);
                    break;
                }
            }
        }

        return invalidIds;
    }

    private static bool IdContainsRepeatedSequence(string id, int numberOfRepetitions) {
        if (id.Length % numberOfRepetitions != 0) {
            return false;
        }

        var lengthOfSequence = id.Length / numberOfRepetitions;

        var firstSequence = id[..lengthOfSequence];
        for (var i = 1; i < numberOfRepetitions; i++) {
            var nextSequence = id.Substring(lengthOfSequence * i, lengthOfSequence);
            if (!firstSequence.Equals(nextSequence)) {
                return false;
            }
        }

        return true;
    }
}
