// A physics box the player can push. Activates a pressure plate when pushed onto it.
// Also acts as an IResettable — R key teleports it back to start position.
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableBox : MonoBehaviour, IResettable
{
    private Rigidbody _rb;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        // Freeze rotation on X and Z to keep the box upright while allowing pushing
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Start()
    {
        // Snapshot position AFTER all Start() calls so it's in its designer-placed position
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void ResetToInitial()
    {
        // Teleport back and zero out all physics velocity
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(_startPosition, _startRotation);
    }
}