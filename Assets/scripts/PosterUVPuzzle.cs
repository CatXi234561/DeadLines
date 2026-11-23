using UnityEngine;
using UnityEngine.Events;

public class PosterUVPuzzle : PuzzleBase
{
    [SerializeField] private string screwdriverItemId = "sonic_screwdriver";
    [SerializeField] private string codeToReveal;
    [SerializeField] private UnityEvent onNeedScrewdriver = new UnityEvent();
    [SerializeField] private UnityEvent onCodeRevealed = new UnityEvent();

    public string RevealedCode => IsSolved ? codeToReveal : string.Empty;

    public override void Interact()
    {
        if (IsSolved)
        {
            onCodeRevealed.Invoke();
            return;
        }

        var progress = Progress;
        if (progress == null)
            return;

        if (!progress.HasItem(screwdriverItemId))
        {
            onNeedScrewdriver.Invoke();
            return;
        }

        CompletePuzzle();
        onCodeRevealed.Invoke();
    }
}
