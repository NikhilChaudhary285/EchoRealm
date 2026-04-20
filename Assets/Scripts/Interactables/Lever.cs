// A toggle lever the player activates by pressing E near it.
// Uses a proximity check and an input prompt in the HUD.
using UnityEngine;

public class Lever : InteractableBase
{
    [Header("Interaction")]
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private Transform leverArm; // child object that rotates visually

    [Header("Rotation")]
    [SerializeField] private float leverAngleOff = -40f;
    [SerializeField] private float leverAngleOn = 40f;

    private Transform _playerTransform;
    private bool _playerInRange = false;
    private Vector3 _originalLocalEuler;

    protected override void Awake()
    {
        base.Awake();
        // Cache initial rotation for reset
        if (leverArm != null)
            _originalLocalEuler = leverArm.localEulerAngles;
    }

    private void Start()
    {
        // Find the player by tag — assign tag "Player" to your player object
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;
        else
            Debug.LogWarning("[Lever] No GameObject tagged 'Player' found.");
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        float dist = Vector3.Distance(transform.position, _playerTransform.position);
        _playerInRange = dist <= interactRange;

        // E key toggles the lever when player is nearby
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
            Toggle();
    }

    private void Toggle()
    {
        SetState(!IsActive); // flips the current state

        if (leverArm != null)
        {
            Vector3 angles = _originalLocalEuler;
            angles.x = IsActive ? leverAngleOn : leverAngleOff;
            leverArm.localEulerAngles = angles;
        }
    }

    // Draw a range gizmo in the editor for easy positioning
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    public override void ResetToInitial()
    {
        if (leverArm != null)
        {
            Vector3 angles = _originalLocalEuler;
            angles.x = leverAngleOff;
            leverArm.localEulerAngles = angles;
        }
        SetState(false);
    }

    // Expose for HUD: show "Press E" prompt when in range
    public bool IsPlayerInRange => _playerInRange;
}