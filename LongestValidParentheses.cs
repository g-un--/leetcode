using System;
using System.Collections.Generic;
using Xunit;

namespace LongestValidParentheses;

// https://leetcode.com/problems/longest-valid-parentheses
public class LongestValidParentheses
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("()", 2)]
    [InlineData("(()", 2)]
    [InlineData(")()())", 4)]
    [InlineData(")()())((()))", 6)]
    public void TestSolution(string test, int expectedLength)
    {
        var solution = new Solution();
        Assert.Equal(expectedLength, solution.LongestValidParentheses(test));
    }
}

public class Solution
{
    public int LongestValidParentheses(string s)
    {
        var stack = new Stack<(char, int)>();
        int max = 0;

        for (var index = 0; index < s.Length; index++)
        {
            var hasItem = stack.TryPeek(out var stackItem);
            var (headChar, headIndex) = stackItem;
            if (hasItem && headChar == '(' && s[index] == ')')
            {
                stack.Pop();
                var (_, oldHeadIndex) = stack.Count > 0 ? stack.Peek() : ('_', -1);
                var diff = index - oldHeadIndex;
                max = diff > max ? diff : max;
            }
            else
            {
                stack.Push((s[index], index));
            }
        }

        return max;
    }
}