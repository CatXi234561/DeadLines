using UnityEngine;
using UnityEngine.Events;

public class FinalDoorPuzzle : PuzzleBase
{
    [SerializeField] private string requiredKeyId = "key";
    [SerializeField] private string correctDateCode = "12021975";
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string openTrigger = "Open";
    [SerializeField] private UnityEvent onKeypadOpened = new UnityEvent();
    [SerializeField] private UnityEvent onMissingKey = new UnityEvent();
    [SerializeField] private UnityEvent onWrongCode = new UnityEvent();
    [SerializeField] private UnityEvent onDoorOpened = new UnityEvent();

    public override void Interact()
    {
        if (!IsSolved)
            onKeypadOpened.Invoke();
    }

    public bool TryUnlock(string enteredCode)
    {
        if (IsSolved)
            return false;

        var progress = Progress;
        if (progress == null)
            return false;

        if (!progress.HasItem(requiredKeyId))
        {
            onMissingKey.Invoke();
            return false;
        }

        if (!string.Equals(enteredCode?.Trim(), correctDateCode?.Trim(), System.StringComparison.Ordinal))
        {
            onWrongCode.Invoke();
            return false;
        }

        CompletePuzzle();

        if (doorAnimator != null && !string.IsNullOrWhiteSpace(openTrigger))
            doorAnimator.SetTrigger(openTrigger);

        onDoorOpened.Invoke();
        GameManager.Instance?.WinGame();
        return true;
    }
}
