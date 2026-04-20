// WHY: This component is the "wire" between any IInteractable and any IReceiver.
// You wire them in the Inspector. No script changes needed to connect new types.
// This is what makes the system modular — the connector depends only on interfaces,
// never on concrete types like "Door" or "PressurePlate".
using UnityEngine;

public class InteractionConnector : MonoBehaviour
{
    [Header("Source — any object with IInteractable")]
    [SerializeField] private MonoBehaviour interactableSource;

    [Header("Targets — any objects with IReceiver (add as many as you like)")]
    [SerializeField] private MonoBehaviour[] receiverTargets;

    // Whether to invert the signal: active interactable = deactivate receiver
    [SerializeField] private bool invertSignal = false;

    private IInteractable _interactable;
    private IReceiver[] _receivers;

    private void Awake()
    {
        // Validate and cache interface references at startup
        _interactable = interactableSource as IInteractable;
        if (_interactable == null)
        {
            Debug.LogError($"[InteractionConnector] {interactableSource?.name} does not implement IInteractable!", this);
            return;
        }

        _receivers = new IReceiver[receiverTargets.Length];
        for (int i = 0; i < receiverTargets.Length; i++)
        {
            _receivers[i] = receiverTargets[i] as IReceiver;
            if (_receivers[i] == null)
                Debug.LogError($"[InteractionConnector] {receiverTargets[i]?.name} does not implement IReceiver!", this);
        }
    }

    private void OnEnable()
    {
        if (_interactable != null)
            _interactable.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks and ghost calls after destroy
        if (_interactable != null)
            _interactable.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(bool isActive)
    {
        // Apply inversion if configured — useful for "normally open" doors, etc.
        bool shouldActivate = invertSignal ? !isActive : isActive;

        foreach (var receiver in _receivers)
        {
            if (receiver == null) continue;
            if (shouldActivate)
                receiver.OnActivated();
            else
                receiver.OnDeactivated();
        }
    }
}