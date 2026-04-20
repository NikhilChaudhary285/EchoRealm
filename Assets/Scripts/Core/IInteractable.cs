// WHY: A shared contract that every trigger-type object implements.
// The connector only cares about this interface — not the concrete type.
// This is the core of the modular system.
using UnityEngine.Events;

public interface IInteractable
{
    bool IsActive { get; }

    // Called by the InteractionConnector to subscribe to state changes.
    // Using UnityAction instead of C# Action keeps it Inspector-friendly.
    event UnityAction<bool> OnStateChanged;
}