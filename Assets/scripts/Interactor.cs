using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Interactor : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor xrInteractor;

    private void Awake()
    {
        if (xrInteractor == null)
            xrInteractor = GetComponent<XRBaseInteractor>();
    }

    private void OnEnable()
    {
        if (xrInteractor == null)
        {
            Debug.LogWarning("Interactor needs an XR Direct, Ray, or other XRBaseInteractor on the same GameObject.", this);
            return;
        }

        xrInteractor.selectEntered.AddListener(HandleSelection);
    }

    private void OnDisable()
    {
        if (xrInteractor != null)
            xrInteractor.selectEntered.RemoveListener(HandleSelection);
    }

    private void HandleSelection(SelectEnterEventArgs args)
    {
        if (args.interactableObject == null)
            return;

        var target = args.interactableObject.transform;
        var behaviours = target.GetComponentsInParent<MonoBehaviour>(true);

        foreach (var behaviour in behaviours)
        {
            if (behaviour is IInteractable interactable)
            {
                interactable.Interact();
                return;
            }
        }
    }
}
