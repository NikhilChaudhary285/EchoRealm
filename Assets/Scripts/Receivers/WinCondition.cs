// The golden platform. Triggers the win state when the player enters.
// Implements IReceiver so a gate/door can also guard it via a connector.
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WinCondition : ReceiverBase
{
    [Header("Win Zone")]
    [SerializeField] private Color winZoneColor = new Color(1f, 0.85f, 0f); // gold
    [SerializeField] private bool requiresActivation = false; // if true, only active when a receiver turns it on

    private bool _isUnlocked;

    protected override void Awake()
    {
        base.Awake();
        GetComponent<Collider>().isTrigger = true;
        _isUnlocked = !requiresActivation; // auto-unlocked if no activation required

        ApplyVisualFeedback(requiresActivation ? inactiveColor : winZoneColor);
    }

    public override void OnActivated()
    {
        base.OnActivated();
        _isUnlocked = true;
        ApplyVisualFeedback(winZoneColor);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        _isUnlocked = false;
        ApplyVisualFeedback(inactiveColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isUnlocked) return;
        if (!other.CompareTag("Player")) return;

        WinManager.Instance?.TriggerWin();
    }

    public override void ResetToInitial()
    {
        _isUnlocked = !requiresActivation;
        isActivated = false;
        ApplyVisualFeedback(requiresActivation ? inactiveColor : winZoneColor);
    }
}