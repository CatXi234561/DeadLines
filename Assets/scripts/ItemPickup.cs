using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemPickup : XRSimpleInteractable, IInteractable
{
    [SerializeField] private string itemId;
    [SerializeField] private UnityEvent onPickedUp = new UnityEvent();

    public void Interact()
    {
        if (GameManager.Instance == null || GameManager.Instance.Progress == null)
        {
            Debug.LogWarning("ItemPickup needs an active GameManager with PuzzleProgress.", this);
            return;
        }

        if (!GameManager.Instance.Progress.AddItem(itemId))
            return;

        onPickedUp.Invoke();
        gameObject.SetActive(false);
    }

    public void MakeAvailable()
    {
        gameObject.SetActive(true);
    }
}
