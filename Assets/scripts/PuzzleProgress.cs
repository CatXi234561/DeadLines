using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProgress : MonoBehaviour
{
    private readonly HashSet<string> collectedItems = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> solvedPuzzles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    public bool AddItem(string itemId)
    {
        if (string.IsNullOrWhiteSpace(itemId))
            return false;

        return collectedItems.Add(itemId.Trim());
    }

    public bool HasItem(string itemId)
    {
        return !string.IsNullOrWhiteSpace(itemId) && collectedItems.Contains(itemId.Trim());
    }

    public bool RemoveItem(string itemId)
    {
        return !string.IsNullOrWhiteSpace(itemId) && collectedItems.Remove(itemId.Trim());
    }

    public bool MarkPuzzleSolved(string puzzleId)
    {
        if (string.IsNullOrWhiteSpace(puzzleId))
            return false;

        return solvedPuzzles.Add(puzzleId.Trim());
    }

    public bool IsPuzzleSolved(string puzzleId)
    {
        return !string.IsNullOrWhiteSpace(puzzleId) && solvedPuzzles.Contains(puzzleId.Trim());
    }

    public void ResetProgress()
    {
        collectedItems.Clear();
        solvedPuzzles.Clear();
    }
}
