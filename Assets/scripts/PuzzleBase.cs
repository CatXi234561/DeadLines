using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public abstract class PuzzleBase : XRSimpleInteractable, IInteractable
{
    [SerializeField] private string puzzleId;
    [SerializeField] private UnityEvent onSolved = new UnityEvent();

    public bool IsSolved { get; private set; }

    public abstract void Interact();

    protected PuzzleProgress Progress
    {
        get
        {
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("No active GameManager was found.", this);
                return null;
            }

            return GameManager.Instance.Progress;
        }
    }

    protected bool CompletePuzzle()
    {
        if (IsSolved)
            return false;

        IsSolved = true;

        if (Progress != null)
            Progress.MarkPuzzleSolved(string.IsNullOrWhiteSpace(puzzleId) ? GetType().Name : puzzleId);

        onSolved.Invoke();
        return true;
    }
}
