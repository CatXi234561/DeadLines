using UnityEngine;
using UnityEngine.Events;

public class RotaryPhonePuzzle : PuzzleBase
{
    [SerializeField] private string correctNumber;
    [SerializeField] private ItemPickup reward;
    [SerializeField] private UnityEvent onPhoneOpened = new UnityEvent();
    [SerializeField] private UnityEvent onWrongNumber = new UnityEvent();

    public override void Interact()
    {
        if (!IsSolved)
            onPhoneOpened.Invoke();
    }

    public bool TrySubmitNumber(string number)
    {
        if (IsSolved)
            return false;

        if (!string.Equals(number?.Trim(), correctNumber?.Trim(), System.StringComparison.Ordinal))
        {
            onWrongNumber.Invoke();
            return false;
        }

        CompletePuzzle();
        reward?.MakeAvailable();
        return true;
    }
}
