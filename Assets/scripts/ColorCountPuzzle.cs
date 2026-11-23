using UnityEngine;
using UnityEngine.Events;

public class ColorCountPuzzle : PuzzleBase
{
    [SerializeField] private string expectedAnswer;
    [SerializeField] private ItemPickup reward;
    [SerializeField] private UnityEvent onPuzzleOpened = new UnityEvent();
    [SerializeField] private UnityEvent onWrongAnswer = new UnityEvent();

    public override void Interact()
    {
        if (!IsSolved)
            onPuzzleOpened.Invoke();
    }

    public bool TrySubmitAnswer(string answer)
    {
        if (IsSolved)
            return false;

        if (!string.Equals(answer?.Trim(), expectedAnswer?.Trim(), System.StringComparison.OrdinalIgnoreCase))
        {
            onWrongAnswer.Invoke();
            return false;
        }

        CompletePuzzle();
        reward?.MakeAvailable();
        return true;
    }
}
