using UnityEngine;
using UnityEngine.Events;

public class ClockPuzzle : PuzzleBase
{
    [SerializeField] private string correctTime;
    [SerializeField] private ItemPickup keyReward;
    [SerializeField] private UnityEvent onPuzzleOpened = new UnityEvent();
    [SerializeField] private UnityEvent onWrongTime = new UnityEvent();

    public override void Interact()
    {
        if (!IsSolved)
            onPuzzleOpened.Invoke();
    }

    public bool TrySetTime(string enteredTime)
    {
        if (IsSolved)
            return false;

        if (!string.Equals(enteredTime?.Trim(), correctTime?.Trim(), System.StringComparison.OrdinalIgnoreCase))
        {
            onWrongTime.Invoke();
            return false;
        }

        CompletePuzzle();
        keyReward?.MakeAvailable();
        return true;
    }
}
