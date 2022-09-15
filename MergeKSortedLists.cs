using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MergeKSortedLists;

// https://leetcode.com/problems/merge-k-sorted-lists/
public class MergeKSortedLists
{
    [Fact]
    public void TestSolution()
    {
        var input = new ListNode[]
        {
            BuildNode(new []{1,4,5}),
            BuildNode(new []{1,3,4}),
            BuildNode(new []{2,6})
        };

        var solution = new Solution();
        var result = solution.MergeKLists(input);
        var resultAsString = BuildString(result);
        Assert.Equal("1,1,2,3,4,4,5,6", resultAsString);
    }

    [Fact]
    public void TestSolutionWithEmptyNodes()
    {
        var input = new ListNode[]
        {
            BuildNode(new int[]{}),
            BuildNode(new []{-1,5,11}),
            BuildNode(new int[]{}),
            BuildNode(new []{6, 10})
        };

        var solution = new Solution();
        var result = solution.MergeKLists(input);
        var resultAsString = BuildString(result);
        Assert.Equal("-1,5,6,10,11", resultAsString);
    }

    ListNode BuildNode(Span<int> numbers, ListNode head = null, ListNode current = null)
    {
        if (numbers.IsEmpty)
            return head;

        var newNode = new ListNode(numbers[0]);
        if (current == null)
        {
            return BuildNode(numbers.Slice(1), newNode, newNode);
        }
        else
        {
            current.next = newNode;
            return BuildNode(numbers.Slice(1), head, newNode);
        }
    }

    string BuildString(ListNode current)
    {
        if (current == null)
            return string.Empty;

        List<int> values = new List<int>();
        while (current != null)
        {
            values.Add(current.val);
            current = current.next;
        }

        return string.Join(',', values);
    }
}

public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        List<ListNode> currentNodes = lists.ToList();
        ListNode head = null;
        ListNode current = null;

        while (currentNodes.Count > 0)
        {
            if(currentNodes.RemoveAll(x => x == null) > 0)
            {
                continue;
            }

            var minNode = currentNodes.MinBy(x => x.val);
            currentNodes.Remove(minNode);

            if (minNode?.next != null)
            {
                currentNodes.Add(minNode.next);
            }
            
            if (current != null)
            {
                current.next = minNode;
                current = minNode;
            }
            else
            {
                current = minNode;
                head = minNode;
            }
        }
        return head;
    }
}

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}