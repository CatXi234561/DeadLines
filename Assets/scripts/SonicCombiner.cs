using UnityEngine;
using UnityEngine.Events;

public class SonicCombiner : PuzzleBase
{
    [SerializeField] private string firstPartId = "sonic_part_a";
    [SerializeField] private string secondPartId = "sonic_part_b";
    [SerializeField] private string screwdriverItemId = "sonic_screwdriver";
    [SerializeField] private UnityEvent onMissingParts = new UnityEvent();
    [SerializeField] private UnityEvent onCombined = new UnityEvent();

    public override void Interact()
    {
        TryCombine();
    }

    public bool TryCombine()
    {
        if (IsSolved)
            return false;

        var progress = Progress;
        if (progress == null)
            return false;

        if (!progress.HasItem(firstPartId) || !progress.HasItem(secondPartId))
        {
            onMissingParts.Invoke();
            return false;
        }

        progress.RemoveItem(firstPartId);
        progress.RemoveItem(secondPartId);
        progress.AddItem(screwdriverItemId);

        CompletePuzzle();
        onCombined.Invoke();
        return true;
    }
}
