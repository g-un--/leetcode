using System;
using Xunit;

namespace leetcode;

// https://leetcode.com/problems/regular-expression-matching/
public class RegularExpressionMatching 
{
    [Theory]
    [InlineData("aa", "a", false)]
    [InlineData("aa", "a*", true)]
    [InlineData("ab", ".*", true)]
    [InlineData("", ".*", true)]
    [InlineData("ab", "ab", true)]
    public void TestSolution(string target, string pattern, bool isMatch)
    {
        var solution = new Solution();
        Assert.Equal(solution.IsMatch(target, pattern), isMatch);
    }
}

public class Solution
{
    public bool IsMatch(string target, string pattern)
    {
        return Match(target, pattern);
    }

    bool Match(ReadOnlySpan<char> target, ReadOnlySpan<char> pattern)
    {
        if (target.IsEmpty && pattern.IsEmpty)
        {
            return true;
        }

        if (pattern.Length >= 2 && pattern[1] == '*')
            return MatchStar(pattern[0], target, pattern.Slice(2));
        else if (target.IsEmpty ^ pattern.IsEmpty)
            return false;
        else if (target[0] == pattern[0] || pattern[0] == '.')
            return Match(target.Slice(1), pattern.Slice(1));
        else
            return false;
    }

    bool MatchStar(char seed, ReadOnlySpan<char> target, ReadOnlySpan<char> pattern)
    {
        if (Match(target, pattern))
        {
            return true;
        }

        while (seed == target[0] || seed == '.')
        {
            target = target.Slice(1);
            var result = Match(target, pattern);
            if (result)
            {
                return true;
            }
        }

        return false;
    }
}