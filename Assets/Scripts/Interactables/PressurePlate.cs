// A pressure plate activates when a Rigidbody rests on it, deactivates when cleared.
// Uses OnTriggerEnter/Exit with a contact counter to handle multiple objects at once.
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PressurePlate : InteractableBase
{
    [Header("Pressure Plate Settings")]
    [SerializeField] private float pressDepth = 0.05f; // how far it visually sinks

    private int _contactCount = 0;       // how many Rigidbodies are on it
    private Vector3 _originalPosition;
    private Vector3 _pressedPosition;

    protected override void Awake()
    {
        base.Awake();
        _originalPosition = transform.localPosition;
        _pressedPosition = _originalPosition - new Vector3(0, pressDepth, 0);

        // The collider MUST be a trigger so we can detect overlaps without physics
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only respond to objects with a Rigidbody (i.e. physics objects like boxes)
        if (other.attachedRigidbody == null) return;

        _contactCount++;
        if (_contactCount == 1) // first contact activates
        {
            SetState(true);
            transform.localPosition = _pressedPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null) return;

        _contactCount = Mathf.Max(0, _contactCount - 1);
        if (_contactCount == 0) // last object left
        {
            SetState(false);
            transform.localPosition = _originalPosition;
        }
    }

    public override void ResetToInitial()
    {
        _contactCount = 0;
        transform.localPosition = _originalPosition;
        SetState(false);
    }
}